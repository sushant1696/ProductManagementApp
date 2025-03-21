using Newtonsoft.Json;

namespace ProductManagementAPI.Model
{
	public class DataModel
	{
	}
	public class CommonResponse<T>
	{
		public CommonResponse()
		{

		}

		public CommonResponse(T Data)
		{
			this.Data = Data;
		}

		public virtual CommonResponse<T> Ok(T Data)
		{
			this.IsSuccess = true;
			this.Status = ErrorCode.Success;
			this.Data = Data;
			this.Message = nameof(ErrorCode.Success);
			return this;
		}
		public virtual CommonResponse<T> Failure(T Data)
		{
			this.IsSuccess = false;
			this.Status = ErrorCode.Failure;
			this.Data = Data;
			this.Message = nameof(ErrorCode.Failure);
			return this;
		}
		

		public CommonResponse<T> NotFound()
		{
			this.IsSuccess = true;
			this.Status = ErrorCode.NoDataFound;
			this.Message = "DataNotFound";
			return this;
		}

		public CommonResponse<T> Failed(ErrorCode status, string? Message = null, string? DeveloperMessage = null)
		{
			this.IsSuccess = false;
			this.Status = status;
			this.Message =Message;
			this.DeveloperMessage = DeveloperMessage;
			return this;
		}

		public static CommonResponse<T> Failure(ErrorCode status, string? Message = null, string? DeveloperMessage = null)
		{
			return new CommonResponse<T>()
			{
				IsSuccess = false,
				Status = status,
				Message = Message,
				DeveloperMessage = DeveloperMessage
			};
		}

		

		public virtual CommonResponse<T> Exception(Exception ex)
		{
			this.IsSuccess = false;
			this.Status = ErrorCode.Exception;
			this.Message = Message;
			this.DeveloperMessage = DeveloperMessage;
			return this;
		}

		[JsonProperty("status", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
		public ErrorCode Status { get; set; }
		public bool IsSuccess { get; set; }

		[JsonProperty("message", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
		public string? Message { get; set; }

		[JsonProperty("data", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
		public T? Data { get; set; }

		[JsonProperty("developerMessage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
		public string? DeveloperMessage { get; set; }
	}
	public enum ErrorCode
	{
		///http status codes
		Success = 200,
		NoDataFound = 201,
		Failure = 300,
		RequestTimeOut = 408,
		BadRequest = 400,
		UnAuthorized = 401,
		Forbidden = 403,
		NotAcceptable = 406,
		Exception = 500,

	}
}
