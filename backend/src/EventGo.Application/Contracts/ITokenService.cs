using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGo.Application.Dtos;

namespace EventGo.Application.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDTO userUpdateDTO);
    }
}