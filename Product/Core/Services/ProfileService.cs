using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Auth;
using Product.Core.Dtos.Response;
using Product.Core.Interfaces;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        private readonly IUploadService _upload;
        public ProfileService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IUploadService upload)
        {
            _userManager = userManager;
            _context = context;
            _upload = upload;
        }

        public async Task<IResponse> CreateAddressAsync(string id, AddressDto addressDto)
        {
            var exitingUser = await _userManager.FindByIdAsync(id);

            if (exitingUser is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Profile not found"
                };
            }

            var newAddress = new Address
            {
                Name = addressDto.Name,
                Status = false,
                UserId = exitingUser.Id,
            };

            _context.Addresses.Add(newAddress);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Address created !"
            };

        }

        public async Task<IResponse> DeleteAddressAsync(string id, Guid addressId)
        {
            var exitingUser = await _userManager.FindByIdAsync(id);

            if (exitingUser is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Profile not found"
                };
            }

            var existingAddress = await _context.Addresses.FindAsync(addressId);


            if (existingAddress is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Address not found"
                };
            }

            _context.Addresses.Remove(existingAddress);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Address deleted"
            };

        }
        public async Task<List<Address>> FindAllAddressAsync(string id)
        {
            return await _context.Addresses.Where(a => a.UserId == id).ToListAsync();
        }

        public async Task<IResponse> GetProfileAsync(string id)
        {
            var exitingUser = await _userManager.FindByIdAsync(id);

            if (exitingUser is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Profile not found"
                };
            }

            var userRoles = await _userManager.GetRolesAsync(exitingUser);

            return new ProfileResponse()
            {
                IsSucceed = true,
                Profile = new Profile()
                {
                    FirstName = exitingUser.FirstName,
                    LastName = exitingUser.LastName,
                    FullName = exitingUser.LastName,
                    Avatar = exitingUser.Avatar,
                    Email = exitingUser.Email,
                    Role = [.. userRoles],
                }
            };
        }

        public async Task<IResponse> UpdateAddressAsync(string id, Guid addressId, AddressDto addressDto)
        {
            var exitingUser = await _userManager.FindByIdAsync(id);

            if (exitingUser is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Profile not found"
                };
            }

            var existingAddress = await _context.Addresses.FindAsync(addressId);


            if (existingAddress is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Address not found"
                };
            }

            existingAddress.Name = addressDto.Name;


            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Address updated !"
            };

        }

        public async Task<ResponseDto> UpdatePasswordAsync(string id, UpdatePasswordDto updatePasswordDto)
        {
            var existingUser = await _userManager.FindByIdAsync(id);

            if (existingUser is null)
            {
                return new ResponseDto { IsSucceed = false, Message = "User not found" };
            }

            var isOldPasswordValid = await _userManager.CheckPasswordAsync(existingUser, updatePasswordDto.OldPassword);

            if (!isOldPasswordValid)
            {
                return new ResponseDto { IsSucceed = false, Message = "Old password is incorrect" };
            }
            var updateResult = await _userManager.ChangePasswordAsync(existingUser, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);

            if (updateResult.Succeeded)
            {
                return new ResponseDto { IsSucceed = true, Message = "Password updated successfully" };
            }

            return new ResponseDto { IsSucceed = false, Message = "Error updating password" };


        }

        public async Task<IResponse> UpdateProfileAsync(string id, UpdateProfileDto updateProfileDto)
        {
            var existingUser = await _userManager.FindByIdAsync(id);

            if (existingUser is null)
            {
                return new ResponseDto { IsSucceed = false, Message = "User not found" };
            }

            existingUser.Email = updateProfileDto.Email;
            existingUser.FirstName = updateProfileDto.FirstName;
            existingUser.LastName = updateProfileDto.LastName;

            var updateResult = await _userManager.UpdateAsync(existingUser);

            if (updateResult.Succeeded)
            {
                return new ProfileResponse()
                {
                    IsSucceed = true,
                    Profile = new Profile()
                    {
                        FirstName = existingUser.FirstName,
                        LastName = existingUser.LastName,
                        Email = existingUser.Email,
                    }
                };
            }

            return new ResponseDto()
            {
                IsSucceed = false,
                Message = "Error updating profile"
            };

        }

        public async Task<ResponseDto> UploadAvatarAsync(string id, IFormFile file)
        {
            var exitingUser = await _userManager.FindByIdAsync(id);

            if (exitingUser is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "User not found"
                };
            }

            var result = await _upload.AddPhotoAsync(file);

            if (result.Error != null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = result.Error.Message
                };
            }

            exitingUser.Avatar = result.SecureUrl.AbsoluteUri;

            await _userManager.UpdateAsync(exitingUser);

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Avatar upload successfully"
            };

        }

    }
}