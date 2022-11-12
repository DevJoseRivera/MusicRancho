using MusicRancho_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicRancho_Web.Models.VM
{
    public class RanchoNumberDeleteVM
    {
        public RanchoNumberDeleteVM()
        {
            RanchoNumber = new RanchoNumberDTO();
        }
        public RanchoNumberDTO RanchoNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RanchoList { get; set; }
    }
}
