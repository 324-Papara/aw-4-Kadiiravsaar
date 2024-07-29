using FluentValidation;
using Papara.Schema.AccountTransactionSchema;

namespace Papara.Bussiness.Validation;

public class AccountTransactionValidator  : AbstractValidator<AccountTransactionRequest>
{
    public AccountTransactionValidator()
    {
        
    }
}