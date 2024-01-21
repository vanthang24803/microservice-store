using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Dtos;
using Auth.Core.interfaces;
using Auth.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        public ProfileService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ProfileServiceResponseDto> GetProfile(string id)
        {
            var exitingUser = await _userManager.FindByIdAsync(id);

            if (exitingUser is null)
            {
                return new ProfileServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User not found",
                    Role = null,
                };
            }

            var userRoles = await _userManager.GetRolesAsync(exitingUser);

            return new ProfileServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Success",
                FirstName = exitingUser.FirstName,
                LastName = exitingUser.LastName,
                FullName = exitingUser.LastName,
                Avatar = exitingUser.Avatar,
                Address = exitingUser.Address,
                Email = exitingUser.Email,
                Role = [.. userRoles],
            };
        }

        public async Task<ResponseDto> UpdatePassword(string id, UpdatePasswordDto updatePasswordDto)
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


        public async Task<UpdateResponseDto> UpdateProfile(string id, UpdateProfileDto updateProfileDto)
        {
            var existingUser = await _userManager.FindByIdAsync(id);

            if (existingUser is null)
            {
                return new UpdateResponseDto()
                {
                    IsSucceed = false,
                    Message = "User not found",
                };
            }

            existingUser.Email = updateProfileDto.Email;
            existingUser.FirstName = updateProfileDto.FirstName;
            existingUser.LastName = updateProfileDto.LastName;
            existingUser.Address = updateProfileDto.Address;

            var updateResult = await _userManager.UpdateAsync(existingUser);

            if (updateResult.Succeeded)
            {
                return new UpdateResponseDto()
                {
                    IsSucceed = true,
                    Message = "Profile updated successfully",
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Email = existingUser.Email,
                };
            }

            return new UpdateResponseDto()
            {
                IsSucceed = false,
                Message = "Error updating profile"
            };

        }
    }
}