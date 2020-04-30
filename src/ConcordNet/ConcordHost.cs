using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConcordNet
{
    public class ConcordHost
    {
        private readonly int _testSeverPort;

        public ConcordHost(int testServerPort = 45678)
        {
            _testSeverPort = testServerPort;
        }

        public IHostBuilder RegisterTestServer<TStartupClass>(
            Action<IServiceCollection> dependencyInjectionConfiguration = null,
            IConfiguration applicationConfiguration = null) where TStartupClass : class
        {
            return new HostBuilder().ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<TStartupClass>();
                builder.UseUrls($"http://0.0.0.0:{_testSeverPort}");

                if (dependencyInjectionConfiguration != null)
                {
                    builder.ConfigureServices(dependencyInjectionConfiguration);
                }

                if (applicationConfiguration != null)
                {
                    builder.UseConfiguration(applicationConfiguration);
                }
            });
        }
    }
}