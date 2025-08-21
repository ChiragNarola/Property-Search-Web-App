using System.Net;

namespace PropertySearch.Business.Errors
{
    public class FileIsLockedError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public FileIsLockedError()
            : base("The file you are trying to access is currently being used by another user or application. Please try again after some time.")
        {
        }
    }
}
