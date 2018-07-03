using Notino.Charts.Commands;
using Notino.Charts.Helm;
using Notino.Charts.IO;
using Notino.Charts.Queries;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFileStorage(this IServiceCollection services, string path)
        {
            services.AddSingleton<IFileSystem>(new FileSystem(path));
            services.AddSingleton<IHelmClient, HelmClient>();
            services.AddTransient<IUploadChartHandler, UploadChartHandler>();
            services.AddTransient<IGetChartIndexYamlHandler, GetChartIndexYamlHandler>();
            services.AddTransient<IGetChartsHandler, GetChartsHandler>();
        }
    }
}
