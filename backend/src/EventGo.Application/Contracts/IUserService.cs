using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGo.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace EventGo.Application.Contracts
{
    public interface IUserService
    {
        Task<bool> UserExistsAsync(string username);
        Task<UserUpdateDTO> GetUserByUsernameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDTO userUpdateDTO, string password);
        Task<UserDTO> CreateAccountAsync(UserDTO userDTO);
        Task<UserUpdateDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO);

    }
}