using FluentValidation;
using Papara.Schema.UserSchema;

namespace Papara.Bussiness.Validation;

public class UserValidator  : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().GreaterThan(0);
    }
}