using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicRancho_RanchoAPI.Models
{
    public class RanchoNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RanchoNo { get; set; }

        [ForeignKey("Rancho")]
        public int RanchoID { get; set; }

        public Rancho Rancho { get; set; }

        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
