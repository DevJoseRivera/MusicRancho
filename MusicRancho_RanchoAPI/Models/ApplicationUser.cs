using Microsoft.AspNetCore.Identity;

namespace MusicRancho_RanchoAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
