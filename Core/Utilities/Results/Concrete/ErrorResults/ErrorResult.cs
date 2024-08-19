using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete.ErrorResults
{
    public class ErrorResult : Result
    {
        private string message;
        private HttpStatusCode notFound;

        public ErrorResult(string message, bool success, HttpStatusCode statusCode) : base(message, success, statusCode)
        {
        }

        public ErrorResult(HttpStatusCode statusCode) : base(false, statusCode)
        {
        }
    }
}
