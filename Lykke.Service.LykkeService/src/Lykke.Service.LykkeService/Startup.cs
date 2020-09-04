using System;
using Autofac;
using AutoMapper;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using Lykke.Sdk.Middleware;
using Lykke.Service.LykkeService.Settings;
using Lykke.SettingsReader;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lykke.Service.LykkeService
{
    [UsedImplicitly]
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        private IReloadingManager<AppSettings> _settingsManager;

        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "LykkeService API",
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

                    logs.AzureTableConnectionStringResolver = settings => settings.LykkeServiceService.Db.LogsConnString;
                    logs.LogsTableName = "LykkeServiceLog";
                };

                options.Extend = (sc, settings) =>
                {
                    sc.AddAutoMapper(typeof(AutoMapperProfile));
                    /*
                    sc
                        .AddOptions()
                        .AddAuthentication(MyAuthOptions.AuthenticationScheme)
                        .AddScheme<MyAuthOptions, KeyAuthHandler>(MyAuthOptions.AuthenticationScheme, null);
                    */
                };

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
        public void Configure(
            IApplicationBuilder app,
            IMapper mapper,
            IHostApplicationLifetime appLifetime)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

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
