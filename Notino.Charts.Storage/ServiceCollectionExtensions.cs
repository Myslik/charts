using Notino.Charts.Commands.Handlers;
using Notino.Charts.Helm;
using Notino.Charts.IO;
using Notino.Charts.Kubernetes;
using Notino.Charts.Queries.Handlers;
using Notino.Charts.Runner;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFileStorage(this IServiceCollection services, string path)
        {
            services.AddSingleton<IFileSystem>(new FileSystem(path));
            services.AddSingleton<IProcessRunner, ProcessRunner>();
            services.AddSingleton<IHelmClient, HelmClient>();
            services.AddSingleton<IKubernetesClient, KubernetesClient>();

            // Commands
            services.AddTransient<IUploadChartHandler, UploadChartHandler>();
            services.AddTransient<IDeleteReleaseHandler, DeleteReleaseHandler>();
            services.AddTransient<IInstallChartHandler, InstallChartHandler>();

            // Queries
            services.AddTransient<IGetChartsHandler, GetChartsHandler>();
            services.AddTransient<IGetChartHandler, GetChartHandler>();
            services.AddTransient<IGetChartIndexHandler, GetChartIndexHandler>();
            services.AddTransient<IGetChartReadmeHandler, GetChartReadmeHandler>();
            services.AddTransient<IGetClustersHandler, GetClustersHandler>();
            services.AddTransient<IGetReleasesHandler, GetReleasesHandler>();
        }
    }
}
