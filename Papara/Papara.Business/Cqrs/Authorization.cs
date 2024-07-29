using MediatR;
using Papara.Base.Response;
using Papara.Schema.AuthorizationSchema;

namespace Papara.Bussiness.Cqrs;

public record CreateAuthorizationTokenCommand(AuthorizationRequest Request) : IRequest<ApiResponse<AuthorizationResponse>>;