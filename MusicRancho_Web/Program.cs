using MusicRancho_Web;
using MusicRancho_Web.Services.IServices;
using MusicRancho_Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));


builder.Services.AddHttpClient<IRanchoService, RanchoService>();
builder.Services.AddScoped<IRanchoService, RanchoService>();

builder.Services.AddHttpClient<IRanchoNumberService, RanchoNumberService>();
builder.Services.AddScoped<IRanchoNumberService, RanchoNumberService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication
    (options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "oidc"; // open id connect
    })
              // we no longer need this because everything  related to identity will be handled by the identity server
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                  options.LoginPath = "/Auth/Login";
                  options.AccessDeniedPath = "/Auth/AccessDenied";
                  options.SlidingExpiration = true;
              })

              // we need to configure the open id connect to use the identity server as the authority and the client id and the client secret and the scope and the response type and the callback path and the post logout redirect uri and the save tokens and the user info endpoint and the token endpoint and the authorization endpoint and the claims mapping 
              .AddOpenIdConnect("oidc", options =>
              {
                  options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"];
                  options.GetClaimsFromUserInfoEndpoint = true;
                  options.ClientId = "music";
                  options.ClientSecret = "secret";
                  options.ResponseType = "code";

                  options.TokenValidationParameters.NameClaimType = "name";
                  options.TokenValidationParameters.RoleClaimType = "role";
                  options.Scope.Add("music");
                  options.SaveTokens = true;

                  options.ClaimActions.MapJsonKey("role", "role"); // for the rancho number

                  // events for the redirect to the identity server
                  options.Events = new OpenIdConnectEvents
                  {

                      // this event will be triggered when the user is redirected to the identity server
                      OnRedirectToIdentityProvider = context =>
                      {
                          context.ProtocolMessage.SetParameter("audience", "music");
                          return Task.CompletedTask;
                      },

                      // this event will be triggered when the user is redirected to the callback path
                      OnRedirectToIdentityProviderForSignOut = context =>
                      {
                          context.ProtocolMessage.SetParameter("audience", "music");
                          return Task.CompletedTask;
                      },

                      // if there are any errors on the remote site
                      OnRemoteFailure = context =>
                      {
                          context.Response.Redirect("/");
                          context.HandleResponse();
                          return Task.FromResult(0);
                      }
                  };
              });

// Add Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrators", policy =>
    {
        policy.RequireRole("admin");
        policy.RequireClaim("role", "admin");
    });
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
