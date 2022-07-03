using System;
namespace XProxy.DAL
{
    public class XSettings : BaseEntity
    {
        public XSettings() { }
        public string UserId { get; set; }
        public string APIUrl { get; set; }
        public string Token { get; set; }
    }
}

