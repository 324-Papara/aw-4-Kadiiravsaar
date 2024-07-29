using Microsoft.IO;
using Papara.Data.Domain;
using Serilog;

namespace Papara.API.Middleware
{

	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate next;
		private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;
		private readonly Action<RequestProfilerModel> requestResponseHandler;
		private const int ReadChunkBufferLength = 4096;
		public RequestLoggingMiddleware(RequestDelegate next, Action<RequestProfilerModel> requestResponseHandler)
		{
			this.next = next;
			this.requestResponseHandler = requestResponseHandler;
			this.recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
		}

		public async Task Invoke(HttpContext context)
		{
			Log.Information("LogRequestLoggingMiddleware.Invoke");

			var model = new RequestProfilerModel
			{
				RequestTime = new DateTimeOffset(),
				Context = context,
				Request = await FormatRequest(context)
			};

			Stream originalBody = context.Response.Body;

			using (MemoryStream newResponseBody = recyclableMemoryStreamManager.GetStream())
			{
				context.Response.Body = newResponseBody;

				await next(context);

				newResponseBody.Seek(0, SeekOrigin.Begin);
				await newResponseBody.CopyToAsync(originalBody);

				newResponseBody.Seek(0, SeekOrigin.Begin);
				model.Response = FormatResponse(context, newResponseBody);
				model.ResponseTime = new DateTimeOffset();
				requestResponseHandler(model);
			}
		}

		private string FormatResponse(HttpContext context, MemoryStream newResponseBody)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;

			return $"Http Response Information: {Environment.NewLine}" +
					$"Schema:{request.Scheme} {Environment.NewLine}" +
					$"Host: {request.Host} {Environment.NewLine}" +
					$"Path: {request.Path} {Environment.NewLine}" +
					$"QueryString: {request.QueryString} {Environment.NewLine}" +
					$"StatusCode: {response.StatusCode} {Environment.NewLine}" +
					$"Response Body: {ReadStreamInChunks(newResponseBody)}";
		}

		private async Task<string> FormatRequest(HttpContext context)
		{
			HttpRequest request = context.Request;

			return $"Http Request Information: {Environment.NewLine}" +
						$"Schema:{request.Scheme} {Environment.NewLine}" +
						$"Host: {request.Host} {Environment.NewLine}" +
						$"Path: {request.Path} {Environment.NewLine}" +
						$"QueryString: {request.QueryString} {Environment.NewLine}" +
						$"Request Body: {await GetRequestBody(request)}";
		}
		public async Task<string> GetRequestBody(HttpRequest request)
		{
			request.EnableBuffering();
			using (var requestStream = recyclableMemoryStreamManager.GetStream())
			{
				await request.Body.CopyToAsync(requestStream);
				request.Body.Seek(0, SeekOrigin.Begin);
				return ReadStreamInChunks(requestStream);
			}
		}

		private static string ReadStreamInChunks(Stream stream)
		{
			stream.Seek(0, SeekOrigin.Begin);
			string result;
			using (var textWriter = new StringWriter())
			using (var reader = new StreamReader(stream))
			{
				var readChunk = new char[ReadChunkBufferLength];
				int readChunkLength;

				do
				{
					readChunkLength = reader.ReadBlock(readChunk, 0, ReadChunkBufferLength);
					textWriter.Write(readChunk, 0, readChunkLength);
				} while (readChunkLength > 0);

				result = textWriter.ToString();
			}

			return result;
		}


	}


	// Ödev için yaptığım eski logger
	//public class LoggerMiddleware
	//{
	//	private readonly RequestDelegate _next;  // bir sonraki Middleware'i temsil edecek, geçirecek
	//	private readonly ILogger<LoggerMiddleware> _logger;

	//	public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
	//	{
	//		_next = next;
	//		_logger = logger;
	//	}

	//	public async Task Invoke(HttpContext context) // contex olay anında işlenen http isteği için 
	//	{
	//		// Log Request
	//		context.Request.EnableBuffering(); // requesti birden fazla kez okumak istersem yaparım
	//										   // Çünkü ASP.NET Core'da varsayılan olarak istek gövdesi bir kez okunur
	//										   // EnableBuffering metodunu kullandım isteğin gövdesini bellekte bir kopyasını tutmamı sağladı

	//		var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync(); // StreamReader kullanarak isteğin gövdesini okuayacak ve değişkene atayacak
	//																						 //  StreamReader kullandım çünkü bir akış üzerinden okuma yapacağım bu akıs üzerinden karakterleri okyacağım
	//																						 // ReadToEndAsync kullandım çünkü asenkron olarak tüm akışı okuyacağım ve string döneceim

	//		context.Request.Body.Position = 0; // işlenen isteğin gövdesinin okuma pozisyonunu 0'a yani başa döndürürüm
	//											// eğer isteğin body alanını başa döndürmezsem diğer bileşenler isteiği okuyamaz boş görür hep

	//		_logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} {requestBody}"); // loga yazıyorum

	//		// Capture response body
	//		var bodyStream = context.Response.Body; // dönecek yanıtın body içerir ve bu yanıt orijinal olan yanıttır

	//		using (var responseBody = new MemoryStream()) // bellekte geçiçi olarak saklamak için memorystream kullandım
	//													  // geçici bir MemoryStream oluşturur ve using bunu kendisi sonlandırır serbest bırakır
	//		{
	//			context.Response.Body = responseBody; // dönen yanıtı geçiçi bellek akışı(MemoryStream) ile değiştirir

	//			await _next(context); //  bir sonraki Middleware'i çağırdık

	//			// Log Response
	//			context.Response.Body.Seek(0, SeekOrigin.Begin);   // dönen yanıtın gövdesinin okuma pozisyonunu 0'a yani başa döndürürüm
	//															   // çünkü daha sonra ki akışın başından itibaren okunmasını sağlamam gerekli
	//															   // eekOrigin.Begin akışınn başına gitmemi sağlar seek metodu başına dönmesi gerektiğini söyler

	//			var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync(); // StreamReader kullanarak isteğin responsunu okuayacak ve değişkene atayacak

	//			context.Response.Body.Seek(0, SeekOrigin.Begin);// tekrar dönen yanıtın gövdesinin okuma pozisyonunu 0'a yani başa döndürürüm
	//															// neden tekrar başa döndüm çünkü orihijan akışa kopyalamadan önce gerekli doğru şekilde kopyalamam gerekli


	//			_logger.LogInformation($"Response: {context.Response.StatusCode} {responseBodyText}"); // loga yazarım 

	//			await responseBody.CopyToAsync(bodyStream); // geçici bellekten aldığım reposnse gövdesini orijinal gövdeye kopyaladım
	//		}
	//	}
	//}  Eskisi bu yani benim eklediğim.


}