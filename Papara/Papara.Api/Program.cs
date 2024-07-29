using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Papara.API.Filters;
using Papara.API.Middleware;
using Papara.API.Modules;
using Papara.Base;
using Papara.Base.Token;
using Papara.Business.Validations;
using Papara.Bussiness.Notification;
using Papara.Bussiness.Token;
using Papara.Data.Domain;
using Serilog;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Text.Json.Serialization;

namespace Papara.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var config = new ConfigurationBuilder() 
		   .AddJsonFile("appsettings.json") 
		   .Build();

			// Serilog konfigürasyonu
			Log.Logger = new LoggerConfiguration() 
				.ReadFrom.Configuration(config)
				.CreateLogger(); 

			Log.Information("Application is starting..."); 

			var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>(); 
			builder.Services.AddSingleton(jwtConfig); 



			builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()))
					.AddFluentValidation(fv => {
						fv.RegisterValidatorsFromAssemblyContaining<CustomerRequestValidator>();
						fv.DisableDataAnnotationsValidation = true; // Veri anotasyonlarýný devre dýþý býrak
					})
					.AddJsonOptions(options =>
					{
						options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
						options.JsonSerializerOptions.WriteIndented = true;
						options.JsonSerializerOptions.PropertyNamingPolicy = null;
					});


			//builder.Services.AddControllers().AddFluentValidation(x =>
			//{
			//	x.RegisterValidatorsFromAssemblyContaining<CustomerRequestValidator>(); // Sonra test edilmeli
			//});


			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
			builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
			{
				containerBuilder.RegisterModule(new AutofacModule(builder.Configuration));
			});

			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = jwtConfig.Issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
					ValidAudience = jwtConfig.Audience,
					ValidateAudience = false,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(2)
				};
			});


			var redisConfig = new ConfigurationOptions();
			redisConfig.DefaultDatabase = 0; 
			redisConfig.EndPoints.Add(builder.Configuration["Redis:Host"], Convert.ToInt32(builder.Configuration["Redis:Port"])); 
			builder.Services.AddStackExchangeRedisCache(opt =>
			{
				opt.ConfigurationOptions = redisConfig; 
				opt.InstanceName = builder.Configuration["Redis:InstanceName"]; 
			});


			builder.Services.AddScoped<ISessionContext>(provider =>
			{
				var context = provider.GetService<IHttpContextAccessor>();
				var sessionContext = new SessionContext();
				sessionContext.Session = JwtManager.GetSession(context.HttpContext);
				sessionContext.HttpContext = context.HttpContext;
				return sessionContext;
			});



			// Add Hangfire configuration
			builder.Services.AddHangfire(configuration => configuration 
				.SetDataCompatibilityLevel(CompatibilityLevel.Version_180) 
				.UseSimpleAssemblyNameTypeSerializer() 
				.UseRecommendedSerializerSettings()
				.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

			builder.Services.AddHangfireServer();

			builder.Services.AddSingleton<INotificationService, NotificationService>();

			builder.Services.AddSingleton<Action<RequestProfilerModel>>(model =>
			{
				Log.Information("-------------Request-Begin------------");
				Log.Information(model.Request);
				Log.Information(Environment.NewLine);
				Log.Information(model.Response);
				Log.Information("-------------Request-End------------");
			});



			builder.Services.AddMemoryCache(); 


			builder.Host.UseSerilog();
			builder.Services.AddScoped<ITokenService, TokenService>();

			var app = builder.Build();


			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(opt =>
				{
					opt.DocExpansion(DocExpansion.None);
				});
			}

			app.UseHangfireDashboard();
			app.UseMiddleware<ErrorHandlerMiddleware>();
			//app.UseMiddleware<LoggerMiddleware>();
			app.UseMiddleware<HeartbeatMiddleware>();
			app.UseMiddleware<RequestLoggingMiddleware>(); 

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
