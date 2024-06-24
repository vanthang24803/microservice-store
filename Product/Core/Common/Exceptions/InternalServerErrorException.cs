using System.Net;
using Product.Core.Common.Utils;

namespace Product.Core.Common.Exceptions
{
    public class InternalServerErrorException(string message = "Internal Server Error") : Exception(message)
    {
        public ApiError ToApiError()
        {
            return new ApiError
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = Message,
                Timestamp = DateTime.Now
            };
        }
    }
}