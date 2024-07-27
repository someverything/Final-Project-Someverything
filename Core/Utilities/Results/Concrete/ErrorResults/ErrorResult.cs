using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete.ErrorResults
{
    internal class ErrorResult : Result
    {
        public ErrorResult(string message, bool success, HttpStatusCode statusCode) : base(message, success, statusCode)
        {
        }

        public ErrorResult(HttpStatusCode statusCode) : base(false, statusCode)
        {
        }
    }
}
