using System.Net;

namespace PropertySearch.Business.Errors
{
    public class UnauthorizedAccessError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
        public UnauthorizedAccessError()
            : base("You are not authorized to access the requested resource.")
        {
        }
    }
}
