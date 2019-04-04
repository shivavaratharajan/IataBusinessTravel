using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace businesstravel
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = Configuration["Jwt:Issuer"],
                           ValidAudience = Configuration["Jwt:Issuer"],
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                       };
                   });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

            // add this...
            services.AddSingleton<IAuthorizationHandler, MinimumMonthsEmployedHandler>();
          //  services.AddSingleton<IConfiguration, MinimumMonthsEmployedHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("ElevatedRights", policy =>
                  policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator"));
                options.AddPolicy("TrainedStaffOnly",
                    policy => policy
                    .RequireClaim("CompletedBasicTraining")
                    .AddRequirements(new MinimumMonthsEmployedRequirement(3)));
            });

            services.AddMvc()
            .AddJsonOptions(options => {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
                
            });
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }



    public class MinimumMonthsEmployedRequirement : IAuthorizationRequirement
    {
        public int MinimumMonthsEmployed { get; private set; }

        public MinimumMonthsEmployedRequirement(int minimumMonths)
        {
            this.MinimumMonthsEmployed = minimumMonths;
        }
    }

    public class MinimumMonthsEmployedHandler
    : AuthorizationHandler<MinimumMonthsEmployedRequirement>
    {
        private const string _authorizedToken = "AuthorizedToken";
        private const string _userAgent = "User-Agent";

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumMonthsEmployedRequirement requirement)
        {
            //var employmentCommenced = context.User
            //    .FindFirst(claim => claim.Type == CustomClaimTypes.EmploymentCommenced).Value;

            //var employmentStarted = Convert.ToDateTime(employmentCommenced);
            //var today = LocalDate.FromDateTime(DateTime.Now);

            //var monthsPassed = Period
            //    .Between(employmentStarted.ToLocalDateTime(), today.AtMidnight(), PeriodUnits.Months)
            //    .Months;



            //context.Resource
            //try
            //{
            //    var headerToken = filterContext.Request.Headers.SingleOrDefault(x => x.Key == _authorizedToken);
            //    if (headerToken.Key != null)
            //    {
            //        authorizedToken = Convert.ToString(headerToken.Value.SingleOrDefault());
            //        userAgent = Convert.ToString(filterContext.Request.Headers.UserAgent);
            //        if (!IsAuthorize(authorizedToken, userAgent))
            //        {
            //            filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            //    return;
            //}


            if (10 >= requirement.MinimumMonthsEmployed)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
