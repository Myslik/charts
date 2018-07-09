using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Notino.Charts.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie.Name = "charts";
            }).AddOpenIdConnect("oidc", options =>
            {
                options.Authority = Configuration["OpenIdConnect:Authority"];
                options.RequireHttpsMetadata = false;
                options.ClientId = Configuration["OpenIdConnect:ClientId"];
                options.ClientSecret = Configuration["OpenIdConnect:ClientSecret"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = Configuration["OpenIdConnect:NameClaim"],
                    RoleClaimType = Configuration["OpenIdConnect:RoleClaim"]
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsOperator", policy => policy.RequireRole("operator"));
                options.AddPolicy("IsArchitect", policy => policy.RequireRole("architect"));
            });

            services
                .AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Releases", "IsOperator");
                    options.Conventions.AddPageRoute("/Charts/Detail", "Charts/{name}/{version?}");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();

            services.AddOptions();
            services.AddLogging();
            services.AddFileStorage(Configuration["ChartDirectory"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
