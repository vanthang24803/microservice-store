using System.Net;
using Product.Core.Common.Utils;

namespace Product.Core.Common.Exceptions
{
    public class ForbiddenException(string message = "Forbidden") : Exception(message)
    {
        public ApiError ToApiError()
        {
            return new ApiError
            {
                Status = (int)HttpStatusCode.Forbidden,
                Message = Message,
                Timestamp = DateTime.Now
            };
        }
    }
}