using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace PostServicioNetCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var IsWindowsService = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Debugger.IsAttached;

            var host = new HostBuilder()
                 .ConfigureHostConfiguration(configHost =>
                 {
                     //Configuración del host
                 })
                 .ConfigureAppConfiguration((hostContext, configApp) =>
                 {
                     //Configuración de la aplicacion

                 })
                .ConfigureServices((hostContext, services) =>
                {
                    //Configuración de los servicios
                    services.AddHostedService<LifetimeHostedService>();
                });
            if (IsWindowsService) //En caso de estar en windows y sin debuger, añadimos al a injeccion de dependencias el servicio
            {
                await host.RunServiceAsync();
            }
            else
            {
                await host.RunConsoleAsync();
            }          
        }
    }
}
