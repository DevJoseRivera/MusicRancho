using Microsoft.AspNetCore.Identity;

namespace MusicRancho_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        // public string PasswordHint { get; set; }

    }
}
