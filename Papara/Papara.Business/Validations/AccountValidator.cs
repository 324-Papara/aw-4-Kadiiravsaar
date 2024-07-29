using FluentValidation;
using Papara.Schema.AccountSchema;

namespace Papara.Bussiness.Validation;

public class AccountValidator  : AbstractValidator<AccountRequest>
{
    public AccountValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().GreaterThan(0);
    }
}