using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EdFi.Ods.SwaggerUI.Core
{
    public class Startup
    {
        // modify to get configuration from file
        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/test/WebSites/ConfigFromFile/Startup.cs
        private const string BaseUrl = "https://api.ed-fi.org/v5.0.0/api";
        private const string Resources = BaseUrl + "/metadata/data/v3/resources/swagger.json";
        private const string Descriptors = BaseUrl + "/metadata/data/v3/descriptors/swagger.json";

        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(
                c =>
                {
                    // modify to allow for custom landing page
                    c.RoutePrefix = string.Empty;
                    c.DocumentTitle = "Ed-Fi ODS API Documentation";

                    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#enable-oauth20-flows
                    c.OAuthClientId("populatedSandbox");
                    c.OAuthClientSecret("populatedSandboxSecret");

                    // modify to use the /metadata endpoint and dynamically add endpoints
                    // modify to parse year specific routes and display them as sections
                    c.SwaggerEndpoint(Resources, nameof(Resources));
                    c.SwaggerEndpoint(Descriptors, nameof(Descriptors));

                    // modify any swagger-ui settings
                    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#apply-swagger-ui-parameters
                    c.DocExpansion(DocExpansion.None);
                    c.ShowExtensions();
                    c.EnableFilter();
                    c.DisplayRequestDuration();
                    c.EnableValidator();

                    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#inject-custom-css
                    c.InjectStylesheet("/swagger.css");

                    // if additional customization is needed we can also inject our own index.html
                    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#customize-indexhtml
                });


            // needed to serve the custom css
            app.UseStaticFiles();
        }
    }
}
