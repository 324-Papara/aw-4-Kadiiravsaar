using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.RabbitMQ
{
	public class RabbitMQPublisher : IDisposable
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;
		private readonly string _queueName;

		public RabbitMQPublisher(IConfiguration configuration)
		{
			var factory = new ConnectionFactory()
			{
				HostName = configuration["RabbitMQ:HostName"],
				UserName = configuration["RabbitMQ:UserName"],
				Password = configuration["RabbitMQ:Password"]
			};

			
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();

			_queueName = "emailQueue";
			_channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
		}

		public void Publish(string message)
		{
			var body = Encoding.UTF8.GetBytes(message);
			_channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}
	
}
