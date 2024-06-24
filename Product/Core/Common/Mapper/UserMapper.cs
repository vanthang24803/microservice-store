using Product.Core.Dtos.Auth;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public class UserMapper
    {
        public static UserDto MapToDto(ApplicationUser user, int totalOrder, int totalPrice)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email,
                TotalOrder = totalOrder,
                TotalPrice = totalPrice,
            };
        }
    }
}