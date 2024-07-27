using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete.ErrorResults
{
    public class ErrorDataResults<T> : DataResult<T>
    {
        public ErrorDataResults(T data, string message, bool success, HttpStatusCode statusCode) : base(data, message, success, statusCode)
        {
        }

        public ErrorDataResults(T data, HttpStatusCode statusCode) : base(data, false, statusCode)
        {
        }

        public ErrorDataResults(string message, HttpStatusCode statusCode) : base(default, message, false, statusCode)
        {
        }

        public ErrorDataResults(HttpStatusCode statusCode) : base(default, false, statusCode)
        {
        }

    }
}
