using System.Net;

namespace PropertySearch.Business.Errors
{
    public interface IErrorWithHttpStatus
    {
        HttpStatusCode StatusCode { get; }
    }
}
