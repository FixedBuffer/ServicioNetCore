using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace PostServicioNetCore
{
    public static class ServiceBaseLifetimeHostExtensions
    {
        public static IHostBuilder UseServiceBaseLifetime(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) => services.AddSingleton<IHostLifetime, ServiceBaseLifetime>());
        }

        public static Task RunServiceAsync(this IHostBuilder hostBuilder, CancellationToken cancellationToken = default)
        {
            //Si es Windows, añadimos el ServiceBaseLifetime a la inyeccion de dependencias
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return hostBuilder.UseServiceBaseLifetime().Build().RunAsync(cancellationToken);
            }
            else //Sino, ejecutamos normalmente
            {
                return hostBuilder.Build().RunAsync(cancellationToken);
            }
        }
    }
}
