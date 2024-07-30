using Microsoft.Extensions.Configuration;
using Papara.Bussiness.Notification;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;

namespace Papara.Business
{
	public class EmailJobService
	{
		private readonly IConfiguration _configuration;
		private readonly INotificationService _notificationService;
		private IModel _channel;
		private IConnection _connection;

		public EmailJobService(IConfiguration configuration, INotificationService notificationService)
		{
			_configuration = configuration;
			_notificationService = notificationService;
			InitializeRabbitMQ();
		}

		private void InitializeRabbitMQ()
		{
			var factory = new ConnectionFactory()
			{
				HostName = _configuration["RabbitMQ:HostName"],
				UserName = _configuration["RabbitMQ:UserName"],
				Password = _configuration["RabbitMQ:Password"]
			};
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: "emailQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
		}

		public void ProcessEmailQueue()
		{
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				var email = JsonConvert.DeserializeObject<dynamic>(message);

				string subject = Convert.ToString(email.Subject);
				string emailTo = Convert.ToString(email.Email);
				string content = Convert.ToString(email.Content);



				_notificationService.SendEmail(subject, emailTo, content);

			};
			_channel.BasicConsume(queue: "emailQueue", autoAck: true, consumer: consumer);
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}



}
