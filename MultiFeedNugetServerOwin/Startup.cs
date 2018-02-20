using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MultiFeedNugetServerOwin.Startup))]

namespace MultiFeedNugetServerOwin
{
    /// <summary>
    /// Represents the entry point into an application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Specifies how the ASP.NET application will respond to individual HTTP request.
        /// </summary>
        /// <param name="app">Instance of <see cref="IAppBuilder"/>.</param>
        public void Configuration(IAppBuilder app)
        {
            CorsConfig.ConfigureCors(ConfigurationManager.AppSettings["cors"]);
            app.UseCors(CorsConfig.Options);

            var configuration = new HttpConfiguration();
            
            AutofacConfig.Configure(configuration);
            app.UseAutofacMiddleware(AutofacConfig.Container);

            FormatterConfig.Configure(configuration);
            RouteConfig.Configure(configuration);
            ServiceConfig.Configure(configuration);

            var jsonSettingsFilePath = GetRequiredAppSetting("nugetserver:settingsfilepath");

            var settings = LightNugetServerSettingsJsonFactory.Create(jsonSettingsFilePath);

            app.UseLightNuGetServer(configuration, settings, feeds =>
            {
                configuration.Services.Replace(typeof(IHttpControllerActivator), new LightNuGetFeedControllerActivator(feeds));
            });

            app.UseWebApi(configuration);
        }

        private static string GetRequiredAppSetting(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception($"AppSetting missing for key '{key}'.");
            }

            return value;
        }
    }
}