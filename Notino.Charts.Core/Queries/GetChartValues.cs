namespace Notino.Charts.Queries
{
    public class GetChartValues : IQuery<string>
    {
        public GetChartValues(string chartName, string version)
        {
            ChartName = chartName ?? throw new System.ArgumentNullException(nameof(chartName));
            Version = version ?? throw new System.ArgumentNullException(nameof(version));
        }

        public string ChartName { get; }
        public string Version { get; }
    }
}
