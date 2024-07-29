using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Base.Response
{
	public partial class ApiResponse
	{
		public string Message { get; set; }
		public bool IsSuccess { get; set; }
		public DateTime ServerDate { get; set; } = DateTime.UtcNow;
		public Guid ReferenceNumber { get; set; } = Guid.NewGuid();

		public ApiResponse(string message = null)
		{	
			if (string.IsNullOrEmpty(message))
			{
				IsSuccess = true;
			}
			else
			{
				IsSuccess = false;
				Message = message;
			}
		}

	}
	public partial class ApiResponse<T>
	{
		public T Data { get; set; }
		public string Message { get; set; }
		public bool IsSuccess { get; set; }
		public DateTime ServerDate { get; set; } = DateTime.UtcNow;
		public Guid ReferenceNumber { get; set; } = Guid.NewGuid();
		public List<string> Errors { get; set; } = new List<string>();



		public ApiResponse()
		{
		}

		public ApiResponse(T data)
		{
			IsSuccess = true;
			Data = data;
			Message = "Success";
		}

		public ApiResponse(bool isSuccess)
		{
			IsSuccess = isSuccess;
			Data = default;
			Message = isSuccess ? "Success" : "Error";
		}

		public ApiResponse(string error)
		{
			IsSuccess = true;
			Data = default;
			Message = error;
		}

		public static ApiResponse<T> Fail(List<string> errors)
		{
			return new ApiResponse<T> { Errors = errors };
		}

	}
}
