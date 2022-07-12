using System;
namespace XProxy.Domain
{
	/// <summary>
    /// Card element, working in BL
    /// </summary>
	public class Av100Filter
	{
        public long Id { get; set; }
        public long UserSettingsId { get; set; }

        public long YearStart { get; set; }
        public long YearEnd { get; set; }

        public long PriceStart { get; set; }
        public long PriceEnd { get; set; }

        public long DistanceStart { get; set; }
        public long DistanceEnd { get; set; }

        public long CarCount { get; set; }
        public long PhoneCount { get; set; }

        public long Regionid { get; set; }        
    }
}

