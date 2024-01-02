using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeHotelMax.Application.Common.Enums
{
    public enum ErrorCode
    {
        AuthError = 401,
        ValidationError = 400,
        NotFound = 404,
        UpdateError = 405,
        DeleteError = 406,
        CreateError = 407,
        ServerError = 500,
        UnknownError = 999,
        
    }
}
