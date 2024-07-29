using AutoMapper;
using Papara.Data.Domain;
using Papara.Schema.CustomerSchema;
using Papara.Schema.CustomerAddressSchema;
using Papara.Schema.CustomerDetailSchema;
using Papara.Schema.CustomerPhoneSchema;
using Papara.Schema.CountrySchema;
using Papara.Schema.UserSchema;
using Papara.Schema.AccountTransactionSchema;
using Papara.Schema.AccountSchema;

namespace Papara.Business.Mapping
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Customer, CustomerResponse>();
			CreateMap<CustomerRequest, Customer>();
			CreateMap<Customer, CustomerResponseWithDetail>()
			.ForMember(dest => dest.CustomerPhones, opt => opt.MapFrom(src => src.CustomerPhones)) // CustomerPhones maplendi
			.ForMember(dest => dest.CustomerAddresses, opt => opt.MapFrom(src => src.CustomerAddresses)); // CustomerAddresses maplendi


			CreateMap<CustomerAddress, CustomerAddressResponse>()
			  .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
			  .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
			  .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
			CreateMap<CustomerAddressRequest, CustomerAddress>();


			CreateMap<CustomerAddress, CustomerAddressResponseWithDetail>() // Customer'ı CustomerResponse olarak mapledim
				.ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));


			CreateMap<CustomerDetail, CustomerDetailResponse>();
			CreateMap<CustomerDetailRequest, CustomerDetail>();

			CreateMap<CustomerDetail, CustomerDetailResponseWithInclude>() // Customer'ı CustomerResponse olarak mapledim
				.ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));


			CreateMap<CustomerPhone, CustomerPhoneResponse>()
				.ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
				.ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
				.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
			CreateMap<CustomerPhoneRequest, CustomerPhone>();

			CreateMap<CustomerPhone, CustomerPhoneResponseWithDetail>()
				.ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));



			CreateMap<CustomerDetail, CustomerDetailResponse>()
			.ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
			.ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
			.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
			CreateMap<CustomerDetailRequest, CustomerDetail>();





			CreateMap<CountryRequest, Country>();
			CreateMap<Country, CountryResponse>();

			CreateMap<User, UserResponse>()
				.ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
				.ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
				.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
			CreateMap<UserRequest, User>();

			CreateMap<Account, AccountResponse>()
				.ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
				.ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
				.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
			CreateMap<AccountRequest, Account>();

			CreateMap<AccountTransaction, AccountTransactionResponse>()
				.ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Account.Customer.IdentityNumber))
				.ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Account.Customer.CustomerNumber))
				.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Account.Customer.FirstName + " " + src.Account.Customer.LastName));
			CreateMap<AccountTransactionRequest, AccountTransaction>();
		}
	}
}
