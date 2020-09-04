using Autofac;
using JetBrains.Annotations;
using Lykke.Job.LykkeService.Settings;
using Lykke.Sdk;
using Lykke.SettingsReader;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lykke.Job.LykkeService
{
    [UsedImplicitly]
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        private IReloadingManager<AppSettings> _settingsManager;

        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "LykkeServiceJob API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            (_configurationRoot, _settingsManager) = services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    // Configure logging from settings or a file
                    /*
                    logs.UseConfiguration = true;
                    // Can override logging configuration from settings with configuration from file
                    logs.ConfigurationFile = "appsettings.Serilog.json"; 
                    */

                    // Confiure logging to push log data to azure table
                    logs.AzureTableConnectionStringResolver = settings => settings.LykkeServiceJob.Db.LogsConnString;
                    logs.LogsTableName = "LykkeServiceJobLog";
                };

                // TODO: Extend the service configuration
                /*
                options.Extend = (sc, settings) =>
                {
                    sc
                        .AddOptions()
                        .AddAuthentication(MyAuthOptions.AuthenticationScheme)
                        .AddScheme<MyAuthOptions, KeyAuthHandler>(MyAuthOptions.AuthenticationScheme, null);
                };
                */

                // TODO: You could add extended Swagger configuration here:
                /*
                options.Swagger = swagger =>
                {
                    swagger.IgnoreObsoleteActions();
                };
                */
            });
        }

        [UsedImplicitly]
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureLykkeContainer(
                _configurationRoot,
                _settingsManager);
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            app.UseLykkeConfiguration(appLifetime, options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                // TODO: Configure additional middleware for eg authentication or maintenancemode checks
                /*
                options.WithMiddleware = x =>
                {
                    x.UseMaintenanceMode<AppSettings>(settings => new MaintenanceMode
                    {
                        Enabled = settings.MaintenanceMode?.Enabled ?? false,
                        Reason = settings.MaintenanceMode?.Reason
                    });
                    x.UseAuthentication();
                };
                */
            });
        }
    }
}
