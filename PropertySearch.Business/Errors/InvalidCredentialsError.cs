using System.Net;

namespace PropertySearch.Business.Errors
{
    public class InvalidCredentialsError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
        public InvalidCredentialsError()
            : base("The provided credentials are seems to be invalid. Please try again.")
        {
        }
    }
}
