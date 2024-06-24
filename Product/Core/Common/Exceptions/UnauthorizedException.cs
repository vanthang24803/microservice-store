using System.Net;
using Product.Core.Common.Utils;

namespace Product.Core.Common.Exceptions
{
    public class UnauthorizedException(string message = "Unauthorized") : Exception(message)
    {
        public ApiError ToApiError()
        {
            return new ApiError
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Message = Message,
                Timestamp = DateTime.Now
            };
        }
    }
}