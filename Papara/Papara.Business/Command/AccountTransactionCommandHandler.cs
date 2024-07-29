using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Bussiness.Cqrs;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.AccountTransactionSchema;


namespace Papara.Bussiness.Command;

public class AccountTransactionCommandHandler :
    IRequestHandler<CreateAccountTransactionCommand, ApiResponse<AccountTransactionResponse>>,
    IRequestHandler<UpdateAccountTransactionCommand, ApiResponse>,
    IRequestHandler<DeleteAccountTransactionCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AccountTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(CreateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountTransactionRequest, AccountTransaction>(request.Request);
        await unitOfWork.AccountTransactionRepository.Insert(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<AccountTransactionResponse>(mapped);
        return new ApiResponse<AccountTransactionResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountTransactionRequest, AccountTransaction>(request.Request);
        mapped.Id = request.AccountTransactionId;
        unitOfWork.AccountTransactionRepository.Update(mapped);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AccountTransactionRepository.Delete(request.AccountTransactionId);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}