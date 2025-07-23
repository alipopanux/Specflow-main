using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lopcommerce.Regles.WebAPI.Tests
{
    public class DefaultWebAppFactory : WebApplicationFactory<Startup>
    {
        private readonly ICollection<Action<IServiceCollection>> _configServices = new List<Action<IServiceCollection>>();
        private readonly IDictionary<string, string> _appConfiguration = new Dictionary<string, string>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing")
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       config.Sources.Clear();
                       config.AddJsonFile("appsettings.json").Build();

                   })
                   .ConfigureAppConfiguration((ctx, conf) =>
                   {
                       conf.AddInMemoryCollection(_appConfiguration);
                   });

            foreach (var config in _configServices)
            {
                builder.ConfigureServices(config);
            }
        }

        public void AddConfigServices(Action<IServiceCollection> configureServices)
        {
            _configServices.Add(configureServices);
        }

        public void AddConfiguration(string key, string value)
        {
            _appConfiguration[key] = value;
        }
    }
}
