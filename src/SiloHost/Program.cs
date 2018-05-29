using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            Random rnd = new Random();
            int Port = rnd.Next(20000, 30000);
            var ip = GetIPAddresses();
            // var connectionString = "Data Source=172.17.0.3,1444;Initial Catalog=Orleans;Integrated Security=True;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";
            var connectionString = "Data Source=localhost;Initial Catalog=Orleans;User Id=orleans; Password=password;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                //.UseLocalhostClustering()
                .UseDashboard(options =>
                {
                    options.HostSelf = true;
                    options.HideTrace = false;
                })
                .UseAdoNetClustering(options =>
                {
                    options.Invariant = "System.Data.SqlClient";
                    options.ConnectionString = connectionString;
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Test";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .Configure<EndpointOptions>(options => options.SiloPort = Port)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain.HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }

        public static IPAddress[] GetIPAddresses()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            return ipHostInfo.AddressList;
        }
    }
}

