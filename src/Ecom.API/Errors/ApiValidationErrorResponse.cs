namespace Ecom.API.Errors
{
    public class ApiValidationErrorResponse : BaseCommonResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}