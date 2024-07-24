using System.Threading.Tasks;

namespace DevExpressREportGeneretor.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
