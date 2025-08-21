using System.Net;

namespace PropertySearch.Business.Errors
{
    public class InvalidGUIDError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public InvalidGUIDError()
            : base("The provided ID seems to be invalid")
        {
        }

        public static void ThrowIfInvalid(string? argument)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new InvalidGUIDError();

            else if (!Guid.TryParse(argument, out _))
                throw new InvalidGUIDError();
        }
    }
}
