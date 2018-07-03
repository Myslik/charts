using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Notino.Charts.Test
{
    public abstract class IntegrationTestContext
    {
        public IConfiguration Configuration { get; set; }

        public IServiceProvider ServiceProvider { get; set; }

        protected IntegrationTestContext()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddFileStorage(Configuration["ChartDirectory"]);
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
