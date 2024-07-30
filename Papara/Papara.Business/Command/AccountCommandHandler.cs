using AutoMapper;
using Hangfire;
using MediatR;
using Newtonsoft.Json;
using Papara.Base.Response;
using Papara.Business.RabbitMQ;
using Papara.Bussiness.Cqrs;
using Papara.Bussiness.Notification;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.AccountSchema;

namespace Papara.Bussiness.Command;

public class AccountCommandHandler :
    IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
    IRequestHandler<UpdateAccountCommand, ApiResponse>,
    IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly INotificationService notificationService;
	private readonly RabbitMQPublisher rabbitMQPublisher;

	public AccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService, RabbitMQPublisher rabbitMQPublisher)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.notificationService = notificationService;
		this.rabbitMQPublisher = rabbitMQPublisher;
	}

	public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
		var mapped = mapper.Map<AccountRequest, Account>(request.Request);
		mapped.OpenDate = DateTime.Now;
		mapped.Balance = 0;
		mapped.AccountNumber = new Random().Next(1000000, 9999999); 
		mapped.IBAN = $"TR{mapped.AccountNumber}97925786{mapped.AccountNumber}01";
		var saved = await unitOfWork.AccountRepository.Insert(mapped);
		await unitOfWork.Complete();

		var customer = await unitOfWork.CustomerRepository.GetById(request.Request.CustomerId);
		BackgroundJob.Schedule(() =>
			SendEmail(customer.Email, $"{customer.FirstName} {customer.LastName}", request.Request.CurrencyCode),
			TimeSpan.FromSeconds(30));

		var response = mapper.Map<AccountResponse>(saved);
		return new ApiResponse<AccountResponse>(response);
	}
    

    [AutomaticRetryAttribute(Attempts = 3,DelaysInSeconds = new []{10,15,18 },OnAttemptsExceeded = AttemptsExceededAction.Fail)]
     public void SendEmail(string email,string name,string currencyCode)
    {
		//notificationService.SendEmail("Yeni hesap acilisi",email,$"Merhaba, {name}, Adiniza ${currencyCode} doviz cinsi hesabiniz acilmistir.");

		var message = JsonConvert.SerializeObject(new
		{
			Email = email,
			Name = name,
			Subject = "Yeni hesap açýlýþý",
			Content = $"Merhaba, {name}, Adýnýza {currencyCode} döviz cinsi hesabýnýz açýlmýþtýr."
		});
		rabbitMQPublisher.Publish(message);
	}

    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountRequest, Account>(request.Request);
        mapped.Id = request.AccountId;
        unitOfWork.AccountRepository.Update(mapped);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AccountRepository.Delete(request.AccountId);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}