using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGo.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace EventGo.Application.Contracts
{
    public interface IAccountService
    {
        Task<bool> UserExistsAsync(string userName);
        Task<UserUpdateDTO> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDTO userUpdateDTO, string password);
        Task<UserUpdateDTO> CreateAccountAsync(UserDTO userDTO);
        Task<UserUpdateDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO);

    }
}