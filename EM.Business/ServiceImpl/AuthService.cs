using AutoMapper;
using EM.Business.BOs;
using EM.Business.Exceptions;
using EM.Business.Services;
using EM.Core.DTOs.Objects;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response.Success;
using EM.Data;
using EM.Data.Entities;
using EM.Data.Exceptions;
using EM.Data.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.ServiceImpl
{
    public class AuthService : IAuthService
    {
        private readonly IOrganizerRepository orgRepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IOrganizerRepository org_Repo, IConfiguration configuration, IMapper mapper)
        {
            orgRepo = org_Repo;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Generate JWT Token with Claims : Name, Email and Id of the organizer
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Email"></param>
        /// <returns>
        /// A bearer token on successful operation and an empty string in case of failure.
        /// </returns>
        public string GenerateToken(string Name, string Email)
        {
            try
            {
                var user =  orgRepo.GetOrganizerByEmail(Email).Result;
                //var userList = orgRepo.GetOrganizers();
                //var user = userList.FirstOrDefault(x=>x.Email==Email);
                if (user == null)
                {
                    return string.Empty;
                }
                var OrgId = user.Id.ToString();
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, Name),
                    new Claim("Email", Email),
                    new Claim("Id", OrgId)
                };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: credentials
                    );
                var tokenVal = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenVal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// validate a user on the basis of email and password and check with the database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>
        /// return false if the user is not present in database.
        /// returns true if it exists.
        /// </returns>
        public OrganizerBo ValidateOrganizer(string email, string password)
        {
            var user = orgRepo.GetOrganizerByEmailAndPassword(email, password);
            if (user == null)
            {
                throw new UserNotFoundException("User Not Found");
            }
            else if (user.Status == 0)
            {
                throw new UserInactiveException("User is Inactive");
            }
            OrganizerBo organizerBo = new OrganizerBo();
            _mapper.Map(user, organizerBo);
            return organizerBo;
        }

        /// <summary>
        /// This services Validates the user and generates a bearer token given in the response along with the organizer information.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public LoginResponseBO OrganizerLogin(LoginDto loginDto)
        {
            var validUser = ValidateOrganizer(loginDto.Email, loginDto.Password);
            var token = GenerateToken(validUser.Name, loginDto.Email);
            LoginResponseBO response = new LoginResponseBO();
            response.Token = token;
            response.Organizer = validUser;
            return response;
        }

    }
}
