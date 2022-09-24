using DevRequired.Service;
using JobPortal.Api.Filter;
using JobPortal.Data.Interfaces.Applicants;
using JobPortal.Data.Interfaces.Forgetpassword;
using JobPortal.Data.Interfaces.Roles;
using JobPortal.Data.Repositories.Applicants;
using JobPortal.Data.Repositories.ForgetPassword;
using JobPortal.Data.Repositories.Jobs;
using JobPortal.Data.Repositories.Roles;
using JobPortal.Service.Admin;
using JobPortal.Service.Applicants;
using JobPortal.Service.ForgetPassword;
using JobPortal.Service.Jobs;
using JobPortal.Service.Roles;
using Jobportel.Data;
using Jobportel.Data.Interfaces;
using Jobportel.Data.Repositories;
using Jobportel.Service.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Jobportel
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
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Conn")));
            services.AddControllers(option => option
                   .Filters.Add(typeof(ExceptionFilter)));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = Configuration["JWT:ValidAudience"],
                     ValidIssuer = Configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                 };
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRecruiterOnly", policy => policy.RequireRole("Admin", "Recruiter"));
                options.AddPolicy("AllAllowed", policy => policy.RequireRole("Admin", "Recruiter", "Candidate"));
                options.AddPolicy("AdminCandidateOnly", policy => policy.RequireRole("Admin", "Candidate"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });


            services.AddScoped<IUserService, UserService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IApplicantRepository, ApplicantRepository>()
                .AddScoped<IApplicantService, ApplicantService>()
                .AddScoped<IJobService, JobService>()
                .AddScoped<IJobRepositry, JobRepositry>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IOtpService, OtpService>()
                .AddScoped<IOtpRepositry, OtpRepository>()
                .AddScoped<IAdminService, AdminService>();
                
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobPortal.Api", Version = "v1" });            
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobPortal.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobPortal.Api V1");
                });


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
