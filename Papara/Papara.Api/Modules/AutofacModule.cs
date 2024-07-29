using Autofac;
using Papara.Data.GenericRepository;
using Papara.Data.UnitOfWork;
using System.Reflection;
using System;
using Module = Autofac.Module;
using AutoMapper;
using Papara.API.ServiceTest;
using Papara.Business.Command.CustomerCommand.Create;
using Papara.Data.Context;
using Papara.Business.Mapping;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Papara.API.Modules
{
	public class AutofacModule : Module
	{
		private readonly IConfiguration _configuration;

		public AutofacModule(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected override void Load(ContainerBuilder builder) // Load metodunu, bağımlılıkları kaydetmek için kullandık.
															   // ContainerBuilder sınıfı ile , bağımlılıkları kaydetmek için gereken yöntemleri sağladık.
		{
			var connectionStringMsSql = _configuration.GetConnectionString("MsSqlConnection"); // yapılandırmadan MSSQL bağlantı için aldık

			// DbContext
			builder.Register(context =>
			{
				var optionsBuilder = new DbContextOptionsBuilder<PaparaDbContext>();
				optionsBuilder.UseSqlServer(connectionStringMsSql);
				return new PaparaDbContext(optionsBuilder.Options);
			}).AsSelf().InstancePerLifetimeScope(); // AsSelf() ifadesi ile, PaparaDbContext' kendisini olark kaydettik.
													// InstancePerLifetimeScope => her HTTP isteği başına bir kez oluşturulacağını belirttik.

			// UnitOfWork
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); // UnitOfWork kayıt ederek IUnitOfWork Kullanılmasını sağladık 

			// AutoMapper
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MapperConfig());
			});

			builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance(); // AutoMapper örneğini IMapper olarak kaydeder ve t
																							// tek bir örnek(singleton) olarak kullanılmasını sağlar.
																							// SingleInstance => singleton



			builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly) 
			  .AsImplementedInterfaces()
			  .InstancePerLifetimeScope(); // CreateCustomerCommand sınıfının bulunduğu alanlara ait tüm türleri kaydettik.
										   // Bu türlerin arayüzleriyle eşleşen tüm uygulamalarını DI'a dahil ettik.
										   //  her HTTP isteği başına bir kez oluşturulacağını belirttik.

			// MediatR
			builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

			builder.Register<ServiceFactory>(context =>
			{
				var c = context.Resolve<IComponentContext>();
				return t => c.Resolve(t);
			}).InstancePerLifetimeScope(); // Bu, MediatR'nin bağımlılıkları çözmek için kullanacağı bir mekanizmadır.




			builder.RegisterType<CustomService>().AsSelf().InstancePerDependency(); //her bağımlılık çözümlemesinde yeni bir örnek oluşturulacağını belirtir.
			builder.RegisterType<CustomService2>().AsSelf().InstancePerLifetimeScope(); //  her yaşam süresi kapsamı için bir örnek oluşturur.
			builder.RegisterType<CustomService3>().AsSelf().SingleInstance(); //  tek bir örnek (singleton) olarak kullanılmasını sağlar.


			// SingleInstance(): Uygulama boyunca sadece bir kez oluşturulacak ve her yerde aynı örnek kullanılacaktır. (Singleton)
			// InstancePerLifetimeScope(): Her yaşam süresi kapsamı için(örneğin, her HTTP isteği) bir kez oluşturulacak.
			// InstancePerDependency(): Her bağımlılık çözümlemesi için yeni bir örnek oluşturulacak. (Transient)


		}
	}
}

