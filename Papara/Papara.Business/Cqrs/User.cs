using MediatR;
using Papara.Base.Response;
using Papara.Schema.UserSchema;


namespace Papara.Bussiness.Cqrs;

public record CreateUserCommand(UserRequest Request) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(long UserId,UserRequest Request) : IRequest<ApiResponse>;
public record DeleteUserCommand(long UserId) : IRequest<ApiResponse>;

public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(long UserId) : IRequest<ApiResponse<UserResponse>>;
public record GetUserByCustomerIdQuery () : IRequest<ApiResponse<List<UserResponse>>>;