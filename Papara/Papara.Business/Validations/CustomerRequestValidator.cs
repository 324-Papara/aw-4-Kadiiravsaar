using FluentValidation;
using Papara.Schema.CustomerSchema;


namespace Papara.Business.Validations
{
	
	public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
	{
		public CustomerRequestValidator()
		{
			RuleFor(c => c.FirstName)
				.NotEmpty().WithMessage("First name is required")
				.MaximumLength(50).WithMessage("First name must be up to 50 characters long");

			RuleFor(c => c.LastName)
				.NotEmpty().WithMessage("Last name is required")
				.MaximumLength(50).WithMessage("Last name must be up to 50 characters long");

			RuleFor(c => c.IdentityNumber)
				.NotEmpty().WithMessage("Identity number is required")
				.MaximumLength(11).WithMessage("Identity number must be exactly 11 characters long");

			RuleFor(c => c.Email)
				.NotEmpty().WithMessage("Email is required")
				.MaximumLength(100).WithMessage("Email must be up to 100 characters long");

			RuleFor(c => c.DateOfBirth)
				.NotEmpty().WithMessage("Date of birth is required");


			//RuleForEach(x => x.CustomerPhones).SetValidator(new CustomerPhoneValidator());
			//RuleForEach(x => x.CustomerAddresses).SetValidator(new CustomerAddressValidator()); //  // hızlı test için kapatıldı
			//RuleFor(x => x.CustomerDetail).SetValidator(new CustomerDetailValidator());
		}
	}

}
