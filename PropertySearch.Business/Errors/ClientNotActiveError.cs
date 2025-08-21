using System.Net;

namespace PropertySearch.Business.Errors
{
    public class ClientNotActiveError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public ClientNotActiveError()
            : base("The client is not active. Client might be de-provisioned by admin.")
        {
        }
    }
}
