using System;

namespace XProxy.DAL
{
    public class UserSettings : BaseEntity
    {
        public UserSettings() { }
        public string UserId { get; set; }


        /// <summary>
        /// Interval for fetch data from AV100 and push to XLombard
        /// </summary>
        public long UpdateInterval { get; set; }

        /// <summary>
        /// Date of last syncing
        /// </summary>
        public DateTime LastSyncDate { get; set; }

        /// <summary>
        /// Active AC100Filter element
        /// </summary>
        public AV100Filter AV100Filter { get; set; }

        /// <summary>
        /// Current connection to AV100
        /// </summary>
        public AV100Settings AV100Settings { get; set; }

        /// <summary>
        /// Current connection to XLombard
        /// </summary>
        public XSettings XLombard { get; set; }
    }
}

