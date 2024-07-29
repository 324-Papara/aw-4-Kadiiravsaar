using FluentValidation;
using Papara.Schema.CustomerAddressSchema;

namespace Papara.Business.Validations
{
	public class CustomerAddressRequestValidator : AbstractValidator<CustomerAddressRequest>
	{
		public CustomerAddressRequestValidator()
		{
			RuleFor(a => a.CustomerId)
				.NotEmpty().WithMessage("Customer ID is required");

			RuleFor(a => a.Country)
				.NotEmpty().WithMessage("Country is required")
				.MaximumLength(50).WithMessage("Country must be up to 50 characters long");

			RuleFor(a => a.City)
				.NotEmpty().WithMessage("City is required")
				.MaximumLength(50).WithMessage("City must be up to 50 characters long");

			RuleFor(a => a.AddressLine)
				.NotEmpty().WithMessage("Address line is required")
				.MaximumLength(250).WithMessage("Address line must be up to 250 characters long");

			RuleFor(a => a.ZipCode)
				.MaximumLength(6).WithMessage("Zip code must be up to 6 characters long");

			RuleFor(a => a.IsDefault)
				.NotEmpty().WithMessage("IsDefault is required");
		}
	}

}
