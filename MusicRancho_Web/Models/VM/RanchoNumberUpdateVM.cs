using MusicRancho_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicRancho_Web.Models.VM
{
    public class RanchoNumberUpdateVM
    {
        public RanchoNumberUpdateVM()
        {
            RanchoNumber = new RanchoNumberUpdateDTO();
        }
        public RanchoNumberUpdateDTO RanchoNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RanchoList { get; set; }
    }
}
