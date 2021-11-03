using JustTradeIt.Software.API.Middlewares;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Implementations;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Implementations;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Amazon.S3;

namespace JustTradeIt.Software.API
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
            services.AddDbContext<JustTradeItDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection"), b => b.MigrationsAssembly("JustTradeIt.Software.API"));
            });

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtTokenAuthentication(Configuration);

            // repository transients
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<ITradeRepository, TradeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // service transients
            var jwtConfig = Configuration.GetSection("JwtConfig");
            services.AddTransient<ITokenService>((c) =>
                new TokenService(
                    jwtConfig.GetSection("secret").Value,
                    jwtConfig.GetSection("expirationInMinutes").Value,
                    jwtConfig.GetSection("issuer").Value,
                    jwtConfig.GetSection("audience").Value
                ));
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<ITradeService, TradeService>();
            services.AddTransient<IUserService, UserService>();

            //AWS
            //services.AddAWSService<IAmazonS3>();
            //services.AddScoped<IAWSS3Service, AWSS3Service>();

            // for retreiving logged in user info from repositories
            services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JustTradeIt.Software.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JustTradeIt.Software.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
