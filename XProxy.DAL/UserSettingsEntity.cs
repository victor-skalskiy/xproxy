using System;

namespace XProxy.DAL
{
    public class UserSettingsEntity : BaseEntityEntity
    {
        public UserSettingsEntity() { }

        public string XLombardAPIUrl { get; set; }

        public string XLombardToken { get; set; }


        public string AV100Token { get; set; }

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
        public AV100FilterEntity AV100Filter { get; set; }

    }
}

