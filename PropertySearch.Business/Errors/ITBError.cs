using System.Net;

namespace PropertySearch.Business.Errors
{
    public class PropertySearchError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode { get; private set; }

        public PropertySearchError(HttpStatusCode statusCode, string message)
            : base(message) => StatusCode = statusCode;

        public PropertySearchError(string message)
            : base(message) => StatusCode = HttpStatusCode.InternalServerError;
    }
}
