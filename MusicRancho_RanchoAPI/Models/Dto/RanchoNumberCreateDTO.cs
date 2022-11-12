using System.ComponentModel.DataAnnotations;

namespace MusicRancho_RanchoAPI.Models.Dto
{
    public class RanchoNumberCreateDTO
    {
        [Required]
        public int RanchoNo { get; set; }
        [Required]
        public int RanchoID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
