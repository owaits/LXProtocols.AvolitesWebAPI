using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Information
{
    public class DeviceInformation
    {
        public string Id { get; set; }

        public int Serial { get; set; }

        public string ComputerName { get; set; }

        public string ConnectedTo { get; set; }

        public string Legend { get; set; }

        public Version SoftwareVersion { get; set; }

        public string Notes { get; set; }
    }
}
