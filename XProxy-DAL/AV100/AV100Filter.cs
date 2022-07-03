using System;
using System.Collections.Generic;

namespace XProxy.DAL
{
    public class AV100Filter
    {
        public AV100Filter() { }

        public long Id { get; set; }

        /// <summary>
        /// Link to settings
        /// </summary>
        public long UserSettingsId { get; set; }

        /// <summary>
        /// Car manufacture period
        /// </summary>
        public long YearStart { get; set; }
        public long YearEnd { get; set; }

        public long PriceStart { get; set; }
        public long PriceEnd { get; set; }

        public long DistanceStart { get; set; }
        public long DistanceEnd { get; set; }

        public ICollection<AV100Region> Regions { get; set; }

        public ICollection<AV100Source> Sources { get; set; }

        /// <summary>
        /// Limit of cars wich linked to current adv phones
        /// </summary>
        public long CarCount { get; set; }

        /// <summary>
        /// Count of advs linked to phone
        /// </summary>
        public long PhoneCount { get; set; }
    }
}