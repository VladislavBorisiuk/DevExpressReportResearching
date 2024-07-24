using System.Threading.Tasks;

namespace DevExpressReportResearching.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
