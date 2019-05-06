using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PostServicioNetCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
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
            if (!Debugger.IsAttached) //En caso de no haber debugger, lanzamos como servicio
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
