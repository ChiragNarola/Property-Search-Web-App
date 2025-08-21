using System.Net;

namespace PropertySearch.Business.Errors
{
    public class NotFoundError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public NotFoundError(string message) : base(message)
        {
        }
    }
}
