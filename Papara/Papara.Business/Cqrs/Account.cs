using MediatR;
using Papara.Base.Response;
using Papara.Schema.AccountSchema;


namespace Papara.Bussiness.Cqrs;

public record CreateAccountCommand(AccountRequest Request) : IRequest<ApiResponse<AccountResponse>>;
public record UpdateAccountCommand(long AccountId,AccountRequest Request) : IRequest<ApiResponse>;
public record DeleteAccountCommand(long AccountId) : IRequest<ApiResponse>;

public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountByIdQuery(long AccountId) : IRequest<ApiResponse<AccountResponse>>;
public record GetAccountByCustomerIdQuery () : IRequest<ApiResponse<List<AccountResponse>>>;