using System;
using System.ComponentModel.DataAnnotations;

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
        public List<long> RegionIds { get; set; }

        [Display(Name = "Sources"), Required(ErrorMessage = "field is required")]
        public List<long> SourceIds { get; set; }
    }
}