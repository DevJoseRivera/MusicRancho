using Duende.IdentityServer.Services;
using MusicRancho_Identity;
using MusicRancho_Identity.Data;
using MusicRancho_Identity.IDbInitializer;
using MusicRancho_Identity.Models;
using MusicRancho_Identity.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// dotnet ef database update -c PersistedGrantDbContext
// dotnet ef database update -c ConfigurationDbContext
// dotnet ef database update -c ApplicationDbContext

//dotnet ef database update -c ApplicationDbContext --project .\MusicRancho_Identity
// dotnet ef database update -c ApplicationDbContext --project .\MusicRancho_RanchoAPI

// added 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// added
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages(); //  of the identity UI

builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

// add authorization services 
builder.Services.AddAuthorization(options =>
{
    //https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("admin"));

    options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("customer"));

    options.AddPolicy("RequireEmployeeRole", policy => policy.RequireRole("employee"));

    options.AddPolicy("RequireAdminClaim", policy => policy.RequireClaim("admin"));

    options.AddPolicy("RequireCustomerClaim", policy => policy.RequireClaim("customer"));

    options.AddPolicy("RequireEmployeeClaim", policy => policy.RequireClaim("employee"));

    options.AddPolicy("AtLeast18", policy => policy.Requirements.Add(new MinimumAgeRequirement(18)));
});

// configure identity  server to use ms sql server entity frameworks keys, clients and scopes store

//builder.Services.AddIdentityServer()
//    .AddConfigurationStore(options =>
//    {
//        options.ConfigureDbContext = b =>
//            b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//                sql => sql.MigrationsAssembly("MusicRancho_Identity"));
//    })
//    .AddOperationalStore(options =>
//    {
//        options.ConfigureDbContext = b =>
//            b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//                sql => sql.MigrationsAssembly("MusicRancho_Identity"));
//    })
//    .AddAspNetIdentity<ApplicationUser>()
//    .AddProfileService<ProfileService>();

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }).AddInMemoryIdentityResources(SD.IdentityResources)
    .AddInMemoryApiScopes(SD.ApiScopes)
    .AddInMemoryClients(SD.Cleints).AddAspNetIdentity<ApplicationUser>()
    .AddDeveloperSigningCredential().AddProfileService<ProfileService>();

builder.Services.AddScoped<IProfileService, ProfileService>(); // this is critical needs to be added after 

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
SeedDatabase();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages();
//app.MapRazorPages().RequireAuthorization(); // then the register page willnot work
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//https://www.identityserver.com/documentation/adminui/Configuration_and_Integration/Configuring_AdminUI/

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
