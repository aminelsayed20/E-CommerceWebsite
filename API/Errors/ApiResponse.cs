namespace API.Errors
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            ErrorMessage = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
			
				return statusCode switch
				{
					400 => "A bad request , you have made",
					401 => "Authorized you are not",
					404 => "Response found it is not",
					500 => "Server error occured",
					_ => null
				};
			
		}
	}
}
