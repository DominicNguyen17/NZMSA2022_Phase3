using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using StudentApp.Domain.Data;
using StudentApp.Infrastructure.Persistence.Context;

namespace StudentApp
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //services
            //    .AddAuthentication()
            //    .AddScheme<AuthenticationSchemeOptions, BackendAuthHandler>("MyAuthentication", null);
            var connection = Configuration.GetConnectionString("NZMSA2022Phase3Context");
            services.AddDbContext<BackendDbContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IBackendRepo, BackendRepo>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
                options.AddPolicy("AuthOnly", policy => {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                        (c.Type == "normalUser" || c.Type == "admin")));
                });
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
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
