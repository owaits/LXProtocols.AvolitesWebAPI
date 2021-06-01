using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public interface IAvolitesTitan
    {
        public IPAddress ConsoleAddress { get; set; }

        public int ConsolePort { get; set; }

        public Titan API { get; }
    }
}
