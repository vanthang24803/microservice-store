
using Microsoft.AspNetCore.Identity;

namespace Product.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Avatar { get; set; } = string.Empty;
        public List<Address> Addresses = [];

        private DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}