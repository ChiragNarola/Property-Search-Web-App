using System.Net;

namespace PropertySearch.Business.Errors
{
    public class BadRequestError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public BadRequestError(string message)
            : base(message)
        {
        }

        public static void ThrowIfNullOrEmpty(string? argument, string message)
        {
            if (string.IsNullOrEmpty(argument))
                throw new BadRequestError(message);
        }

        public static void ThrowIfNullOrWhiteSpace(string? argument, string message)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new BadRequestError(message);
        }
    }
}
