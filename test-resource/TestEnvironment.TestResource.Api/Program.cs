using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TestEnvironment.TestResource.Api
{
    public class Program
    {
        #region Public Methods

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<TestStartup>()
                .Build();

            host.Run();
        }

        #endregion Public Methods
    }
}
