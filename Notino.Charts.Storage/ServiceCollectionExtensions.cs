using Notino.Charts.Commands.Handlers;
using Notino.Charts.Helm;
using Notino.Charts.IO;
using Notino.Charts.Queries.Handlers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFileStorage(this IServiceCollection services, string path)
        {
            services.AddSingleton<IFileSystem>(new FileSystem(path));
            services.AddSingleton<IHelmClient, HelmClient>();
            services.AddTransient<IUploadChartHandler, UploadChartHandler>();
            services.AddTransient<IGetChartsHandler, GetChartsHandler>();
            services.AddTransient<IGetChartHandler, GetChartHandler>();
            services.AddTransient<IGetChartIndexHandler, GetChartIndexHandler>();
            services.AddTransient<IGetChartReadmeHandler, GetChartReadmeHandler>();
        }
    }
}
