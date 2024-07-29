using FluentValidation;
using Papara.Schema.CustomerDetailSchema;

namespace Papara.Business.Validations
{
	public class CustomerDetailRequestValidator : AbstractValidator<CustomerDetailRequest>
	{
		public CustomerDetailRequestValidator()
		{
			RuleFor(d => d.FatherName)
				.MaximumLength(50).WithMessage("Father name must be up to 50 characters long");

			RuleFor(d => d.MotherName)
				.MaximumLength(50).WithMessage("Mother name must be up to 50 characters long");

			RuleFor(d => d.EducationStatus)
				.MaximumLength(100).WithMessage("Education status must be up to 100 characters long");

			RuleFor(d => d.MonthlyIncome)
				.MaximumLength(50).WithMessage("Monthly income  must be up to 50 characters long");

			RuleFor(d => d.Occupation)
				.MaximumLength(100).WithMessage("Occupation must be up to 100 characters long");
		}
	}

}
