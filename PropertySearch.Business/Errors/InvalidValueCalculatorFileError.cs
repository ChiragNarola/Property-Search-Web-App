using System.Net;

namespace PropertySearch.Business.Errors
{
    public class InvalidValueCalculatorFileError : Exception, IErrorWithHttpStatus
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidValueCalculatorFileError(string message)
            : base("The provided valuecalculator file is seems to be invalid.")
        {
        }
    }
}