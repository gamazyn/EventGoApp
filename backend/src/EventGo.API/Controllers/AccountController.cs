using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventGo.API.Extensions;
using EventGo.Application.Contracts;
using EventGo.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventGo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
                                 ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar usuário. Error: {ex.Message}");
            }
        }

        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            try
            {
                if (await _accountService.UserExistsAsync(userDTO.UserName))
                    return BadRequest("Usuário já existe.");

                var user = await _accountService.CreateAccountAsync(userDTO);
                if (user != null)
                    return Ok(new
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        Token = _tokenService.CreateToken(user).Result
                    });

                return BadRequest("Usuário não criado. Tente novamente.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar registrar usuário. Error: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.UserName);
                if (user == null) return Unauthorized("Usuário ou senha inválidos.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized("Usuário ou senha incorretos.");

                return Ok(new
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    Token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar realizar login. Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                if (userUpdateDTO.UserName != User.GetUserName()) return Unauthorized("Usuário Inválido");

                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Usuário inválido.");

                var userReturn = await _accountService.UpdateUserAsync(userUpdateDTO);
                if (userReturn == null)
                    return NoContent();

                return Ok(new
                {
                    UserName = userReturn.UserName,
                    FirstName = userReturn.FirstName,
                    Token = _tokenService.CreateToken(userReturn).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar atualizar usuário. Error: {ex.Message}");
            }
        }
    }
}