using System;
using System.Net;
using System.Net.Http;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Titan
    {
        private HttpClient http = null;

        public Titan(IPAddress consoleAddress):this(consoleAddress, 4430)
        {
        }

        public Titan(IPAddress consoleAddress, int port)
        {
            http = new HttpClient() { BaseAddress = new Uri($"http://{consoleAddress}:{port}") };
            Playbacks = new Playbacks(http);
            Fixtures = new Fixtures(http);
        }

        public Playbacks Playbacks { get; private set; }

        public Fixtures Fixtures { get; private set; }

    }
}
