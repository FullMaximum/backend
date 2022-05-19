namespace FlowersBEWebApi.Controllers
{
    public class BaseResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public object? ReturnObject { get; set; }

        public BaseResult(bool success, int statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }

        public BaseResult(bool success, int statusCode, string message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }

        public BaseResult(bool success, int statusCode, string message, object obj)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            ReturnObject = obj;
        }
    }
}
