using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using MusicRancho_Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MusicRancho_Identity
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public ProfileService(
            UserManager<ApplicationUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await _userMgr.FindByIdAsync(sub);

            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(u => context.RequestedClaimTypes.Contains(u.Type)).ToList();
            IList<Claim> allclaims = await _userMgr.GetClaimsAsync(user);
            var twoFact = await _userMgr.GetTwoFactorEnabledAsync(user);

            claims.Add(new Claim(JwtClaimTypes.Name, user.Name));
            if (_userMgr.SupportsUserRole)
            {
                IList<string> roles = await _userMgr.GetRolesAsync(user);
                foreach (var rolename in roles)
                {
                    // add a new claim called NickName to the user with the value of the role

                    claims.Add(new Claim(JwtClaimTypes.Role, rolename));
                    // you can add more claims here

                }

                foreach (var claim in allclaims)
                {
                    claims.Add(claim);
                }
            }

            context.IssuedClaims = claims; // need to append the claim to the   request context
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await _userMgr.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
