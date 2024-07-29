using FluentValidation;
using Papara.Schema.CustomerPhoneSchema;

namespace Papara.Business.Validations
{
	public class CustomerPhoneRequestValidator : AbstractValidator<CustomerPhoneRequest>
	{
		public CustomerPhoneRequestValidator()
		{
			RuleFor(p => p.CountryCode)
				.NotEmpty().WithMessage("Country code is required")
				.MaximumLength(5).WithMessage("Country code must be up to 5 characters long");

			RuleFor(p => p.Phone)
				.NotEmpty().WithMessage("Phone number is required")
				.MaximumLength(10).WithMessage("Phone number must be up 10 digits long");

			RuleFor(p => p.IsDefault)
				.NotNull().WithMessage("IsDefault is required");
		}
	}

}
