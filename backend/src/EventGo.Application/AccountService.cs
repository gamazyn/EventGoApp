using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventGo.Application.Contracts;
using EventGo.Application.Dtos;
using EventGo.Domain.Identity;
using EventGo.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventGo.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersistence _persistence;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
                             IMapper mapper, IUserPersistence persistence)
        {
            _persistence = persistence;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDTO userUpdateDTO, string password)
        {
            try
            {
                var user = await _userManager.Users
                                .SingleOrDefaultAsync(user => user.UserName == userUpdateDTO.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserDTO> CreateAccountAsync(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserDTO>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar criar usuário por username. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDTO> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _persistence.GetUserbyUserNameAsync(userName);
                if (user == null) return null;

                var userUpdateDTO = _mapper.Map<UserUpdateDTO>(user);
                return userUpdateDTO;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar buscar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = await _persistence.GetUserbyUserNameAsync(userUpdateDTO.UserName);
                if (user == null) return null;

                _mapper.Map(userUpdateDTO, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDTO.Password);

                _persistence.Update<User>(user);

                if (await _persistence.SaveChangesAsync())
                {
                    var userReturn = await _persistence.GetUserbyUserNameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDTO>(userReturn);
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            try
            {
                return await _userManager.Users
                                         .AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar se já existe usuário. Erro: {ex.Message}");
            }
        }
    }
}