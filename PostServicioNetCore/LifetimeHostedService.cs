using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PostServicioNetCore
{
    public class LifetimeHostedService : IHostedService, IDisposable
    {
        ILogger<LifetimeHostedService> logger;
        private Timer _timer;
        private string _path;
        public LifetimeHostedService(ILogger<LifetimeHostedService> logger, IHostingEnvironment hostingEnvironment)
        {
            this.logger = logger;
            _path = $"{hostingEnvironment.ContentRootPath}PostServicioCore.txt"; 
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _timer = new Timer((e) => WriteTimeToFile(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message},{ex.StackTrace}");
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void WriteTimeToFile()
        {
            WriteToFile(DateTime.Now.ToString());
        }

        private void WriteToFile(string message)
        {
            if (!File.Exists(_path))
            {
                using (var sw = File.CreateText(_path))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (var sw = File.AppendText(_path))
                {
                    sw.WriteLine(message);
                }
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
