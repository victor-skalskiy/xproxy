using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XProxy.Web.Models
{
    public class FilterEditModel
    {
        [Display(Name = "Year start"), Required(ErrorMessage = "field is required")]
        public long YearStart { get; set; }
        [Display(Name = "Year end"), Required(ErrorMessage = "field is required")]
        public long YearEnd { get; set; }

        [Display(Name = "Price (start)"), Required(ErrorMessage = "field is required")]
        public long PriceStart { get; set; }
        [Display(Name = "Price (end)"), Required(ErrorMessage = "field is required")]
        public long PriceEnd { get; set; }

        [Display(Name = "Distance (start)"), Required(ErrorMessage = "field is required")]
        public long DistanceStart { get; set; }
        [Display(Name = "Distance (end)"), Required(ErrorMessage = "field is required")]
        public long DistanceEnd { get; set; }

        [Display(Name = "Adv count of car"), Required(ErrorMessage = "field is required")]
        public long CarCount { get; set; }
        [Display(Name = "Adv count on phone"), Required(ErrorMessage = "field is required")]
        public long PhoneCount { get; set; }

        [Display(Name = "Regions"), Required(ErrorMessage = "field is required")]
        public long[] RegionIds { get; set; } = Array.Empty<long>();

        [Display(Name = "Sources"), Required(ErrorMessage = "field is required")]
        public long[] SourceIds { get; set; } = Array.Empty<long>();
        
        public MultiSelectList? Regions { get; set; }
        
        public MultiSelectList? Sources { get; set; }

        public string FilterRequestString { get; set; }
    }
}