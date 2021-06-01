using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public class AvolitesTitanService : IAvolitesTitan
    {
        private Titan titan = null;

        public AvolitesTitanService()
        {
            this.titan = new Titan(ConsoleAddress, ConsolePort);
        }

        public IPAddress ConsoleAddress { get; set; } = IPAddress.Parse("10.10.0.118");

        public int ConsolePort { get; set; } = 4430;

        public Titan API { get { return titan; } }
    }

    public static class AvolitesTitanServiceExtensions
    {
        public static IServiceCollection AddAvolitesTitan(this IServiceCollection services)
        {
            return services.AddScoped<IAvolitesTitan,AvolitesTitanService>();
        }

    }
}
