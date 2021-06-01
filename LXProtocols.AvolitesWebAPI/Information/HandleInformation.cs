using System;
using System.Collections.Generic;
using System.Text;

namespace LXProtocols.AvolitesWebAPI
{
    public class HandleInformation
    {
        public int TitanId { get; set; }

        public string Type { get; set; }

        public string Legend { get; set; }

        public string Notes { get; set; }

        public string Icon { get; set; }

        public bool Selected { get; set; }

        public bool Active { get; set; }
    }
}
