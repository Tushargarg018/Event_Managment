using EM.Business.BOs;
using EM.Core.DTOs.Objects;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response.Success;
using EM.Data;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{

    public interface IAuthService
    {
        OrganizerBo ValidateOrganizer(string email, string password);
        public string GenerateToken(string Name, string Email);
        public LoginResponseBO OrganizerLogin(LoginDto loginDto);

    }
}

