using AutoMapper;
using MediatR;
using Papara.Base;
using Papara.Base.Response;
using Papara.Bussiness.Cqrs;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.AccountTransactionSchema;

namespace Papara.Bussiness.Query;

public class AccountTransactionQueryHandler : 
    IRequestHandler<GetAllAccountTransactionQuery,ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetAccountTransactionByIdQuery,ApiResponse<AccountTransactionResponse>>,
    IRequestHandler<GetAccountTransactionByCustomerIdQuery, ApiResponse<List<AccountTransactionResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionContext sessionContext;

    public AccountTransactionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,ISessionContext sessionContext)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionContext = sessionContext;
    }
    
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAllAccountTransactionQuery request, CancellationToken cancellationToken)
    {
        List<AccountTransaction> entityList = await unitOfWork.AccountTransactionRepository.GetAll("Customer");
        var mappedList = mapper.Map<List<AccountTransactionResponse>>(entityList);
        return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(GetAccountTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.AccountTransactionRepository.GetById(request.AccountTransactionId,"Customer");
        var mapped = mapper.Map<AccountTransactionResponse>(entity);
        return new ApiResponse<AccountTransactionResponse>(mapped);
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAccountTransactionByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        List<AccountTransaction> entityList = await unitOfWork.AccountTransactionRepository.Where(x=> x.Account.CustomerId == sessionContext.Session.CustomerId, "Customer");
        var mappedList = mapper.Map<List<AccountTransactionResponse>>(entityList);
        return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
    }
}