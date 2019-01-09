using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using BLL.Interface;
using BLL.Implementation;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
//using LIMO;
using DAL.DomainModels;
using Korsa;
using Component.Utility;
using Hangfire;

namespace NetCoreWebApi
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            BuildAppSettingsProvider();
        }
        private void BuildAppSettingsProvider()
        {   
            AppSettingsProvider.ConnectionString = Configuration["ConnectionString"];
            AppSettingsProvider.SocketServerIP = Configuration["SocketServerIP"];
            AppSettingsProvider.SocketListeningPort = Configuration["SocketListeningPort"];
            AppSettingsProvider.AndroidUserServerKey = Configuration["AndroidUserServerKey"];
            AppSettingsProvider.FCMSenderIdUser = Configuration["FCMSenderIdUser"];
            AppSettingsProvider.FCMSenderIdDriver = Configuration["FCMSenderIdDriver"];
            AppSettingsProvider.FCMServerKeyDriverApp = Configuration["FCMServerKeyDriverApp"];
            AppSettingsProvider.FCMServerKeyUserApp = Configuration["FCMServerKeyUserApp"];
            AppSettingsProvider.UserImageFolderPath = Configuration["UserImageFolderPath"];
            AppSettingsProvider.DriverImageFolderPath = Configuration["DriverImageFolderPath"];
            AppSettingsProvider.CarImageFolderPath = Configuration["CarImageFolderPath"];
            AppSettingsProvider.RegistrationCopyImageFolderPath = Configuration["RegistrationCopyImageFolderPath"];
            AppSettingsProvider.LicenseImageFolderPath = Configuration["LicenseImageFolderPath"];
            AppSettingsProvider.TopupReceiptsImageFolderPath = Configuration["TopupReceiptsImageFolderPath"];
            AppSettingsProvider.RideImageFolderPath = Configuration["RideImageFolderPath"];
            AppSettingsProvider.GoogleMapsAPIKey = Configuration["GoogleMapsAPIKey"];
            AppSettingsProvider.NexmoApiKey = Configuration["Nexmo.api_key"];
            AppSettingsProvider.NexmoApiSecret = Configuration["Nexmo.api_secret"];
            AppSettingsProvider.NexmoFromNumber = Configuration["NEXMO_FROM_NUMBER"];
            AppSettingsProvider.NexmoApplicationId = Configuration["Nexmo.Application.Id"];
            AppSettingsProvider.NexmoMessageSenderName = Configuration["NexmoMessageSenderName"];
            AppSettingsProvider.PaypalAPIVerificationUrl = Configuration["PaypalAPIVerificationUrl"];
            AppSettingsProvider.PaypalAPIMakePaymentUrl = Configuration["PaypalAPIMakePaymentUrl"];
            AppSettingsProvider.PaypalAPIGetTokenUrl = Configuration["PaypalAPIGetTokenUrl"];
            AppSettingsProvider.APIBaseURL = Configuration["APIBaseURL"];
            AppSettingsProvider.PaypalUsername = Configuration["PaypalUsername"];
            AppSettingsProvider.PaypalPassword = Configuration["PaypalPassword"];
            AppSettingsProvider.RideLaterService = Configuration["RideLaterService"];

            // configuration parameters for database backup
            AppSettingsProvider.DataBaseName = Configuration["DataBaseName"];
            AppSettingsProvider.DBUser = Configuration["DBUser"];
            AppSettingsProvider.Password = Configuration["Password"];
            AppSettingsProvider.DataSource= Configuration["DataSource"];
            AppSettingsProvider.DatabaseBackUpUrl = Configuration["DatabaseBackUpUrl"]; 
            AppSettingsProvider.DatabaseBackUpZipUrl = Configuration["DatabaseBackUpZipUrl"];
            AppSettingsProvider.FromMailAddress = Configuration["FromMailAddress"];





        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtIssuer"),
                        ValidAudience = Configuration.GetValue<string>("JwtAudience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSecretKey")))
                    };
                });

            services.AddDbContext<DataContext>(options => options.UseSqlServer(AppSettingsProvider.ConnectionString, b => b.UseRowNumberForPaging()));

            services.AddTransient<IBOUser, BOUser>();
            services.AddTransient<IBODriver, BODriver>();
            services.AddTransient<IBORide, BORide>();
            services.AddTransient<IBONotification, BONotification>();
            services.AddTransient<IBOAdmin, BOAdmin>();
            services.AddTransient<IBOBackup, BOBackup>();

            services.AddMvc();
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/swagger/index", "");
            });
            services.AddHangfire(configuration =>
            {
                   configuration.UseSqlServerStorage(AppSettingsProvider.ConnectionString);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Korsa API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
            });

            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
                // options.AutomaticAuthentication = true;
            });
            AutoMapperConfig.CreateConfig();
            var provider = services.BuildServiceProvider();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DataContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("Staging_2"))
            {
                app.UseExceptionHandler("/Error");
               
            }


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "FileDirectory")),
                RequestPath = "/FileDirectory"
            });


            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            dbContext.Database.EnsureCreated();
            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
