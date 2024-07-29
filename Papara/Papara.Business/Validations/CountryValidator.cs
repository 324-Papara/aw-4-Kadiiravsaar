using FluentValidation;
using Papara.Schema.CountrySchema;

namespace Papara.Business.Validations
{
	public class CountryValidator : AbstractValidator<CountryRequest>
	{
		public CountryValidator()
		{
			RuleFor(x => x.CountryCode).NotEmpty().MinimumLength(3).MaximumLength(3);
			RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
		}
	}

}
