using System;
namespace XProxy.Domain
{
    public class XLombardResponse
    {
        public XLombardResponse() { }

        public bool state { get; set; }
        public string error { get; set; }
        public string data { get; set; }
    }

}

