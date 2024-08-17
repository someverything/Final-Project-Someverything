using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO)
    }
}
