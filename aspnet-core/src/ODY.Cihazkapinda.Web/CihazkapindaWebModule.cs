using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ODY.Cihazkapinda.EntityFrameworkCore;
using ODY.Cihazkapinda.Localization;
using ODY.Cihazkapinda.MultiTenancy;
using ODY.Cihazkapinda.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Lsw.Abp.AspNetCore.Mvc.UI.Theme.Stisla;
using Lsw.Abp.AspNetCore.Mvc.UI.Theme.Stisla.Bundling;
using Volo.Abp.MultiTenancy;
using ODY.Cihazkapinda.Web.Hubs;

namespace ODY.Cihazkapinda.Web
{
    [DependsOn(
        typeof(CihazkapindaHttpApiModule),
        typeof(CihazkapindaApplicationModule),
        typeof(CihazkapindaEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreMvcUiStislaThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
        )]
    public class CihazkapindaWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(CihazkapindaResource),
                    typeof(CihazkapindaDomainModule).Assembly,
                    typeof(CihazkapindaDomainSharedModule).Assembly,
                    typeof(CihazkapindaApplicationModule).Assembly,
                    typeof(CihazkapindaApplicationContractsModule).Assembly,
                    typeof(CihazkapindaWebModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpTenantResolveOptions>(options =>
            {
                options.AddDomainTenantResolver("{0}.localhost:44338");
            });

            ConfigureUrls(configuration);
            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
            ConfigureSignalRServices(context.Services);
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                options.StyleBundles.Configure(
                    StislaThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/global-styles.css");
                    }
                );
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "Cihazkapinda";
                });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CihazkapindaWebModule>();
            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<CihazkapindaDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ODY.Cihazkapinda.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<CihazkapindaDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ODY.Cihazkapinda.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<CihazkapindaApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ODY.Cihazkapinda.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<CihazkapindaApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ODY.Cihazkapinda.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<CihazkapindaWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new CihazkapindaMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(CihazkapindaApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cihazkapinda API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }


        private void ConfigureSignalRServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cihazkapinda API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                endpoints.MapHub<SignalRHub>("/chatHub");
            });
        }
    }
}
