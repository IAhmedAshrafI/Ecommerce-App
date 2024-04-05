namespace Ecom.API.Errors
{
    public class ApiException : BaseCommonResponse
    {
        public ApiException(int stuatusCode, string message = null, string details = null) : base(stuatusCode, message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}