using Microsoft.AspNetCore.Authorization;

namespace MusicRancho_Identity.Policies
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "Age"))
            {
                return Task.CompletedTask;
            }
            /*else
            {
                context.Fail();
            }*/

            var age = int.Parse(context.User.FindFirst(c => c.Type == "Age").Value);

            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }


            return Task.CompletedTask;
        }
    }

}
