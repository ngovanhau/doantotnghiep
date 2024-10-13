using System.Data;
using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Application.Exceptions;
using Common.Application.Models;
using Common.Application.Settings;
using Common.Services;
using Common.Utilities;
using Domain.Dtos;
using Domain.Entities;
using Repositories;
using Utilities;
using RPMSMaster.Common.Application.Settings;
using Domain.Enums;
using MimeKit;
using System.Text;
using System.Security.Cryptography;
using MimeKit.Text;
using MailKit.Net.Smtp;
using BPMaster.Domains.Entities;


namespace Services
{
    [ScopedService]
    public class IdentityUserService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly IdentityUserRepository _repo = new(connection);
        private readonly PasswordSetting _passwordSetting = setting.PasswordSetting;
        private readonly JwtTokenSetting _jwtTokenSetting = setting.JwtTokenSetting;
        private readonly EmailSetting _emailSetting = setting.EmailSetting;

        public async Task<IdentityUser> RegisterUserAsync(RegisterUserDto dto)
        {
            var usercheck = await _repo.GetByUsernameAsync(dto.Username);
            if (usercheck == null)
            {
                var user = _mapper.Map<IdentityUser>(dto);

                var hashPassword = PasswordUtil.HashPBKDF2(dto.Password, _passwordSetting, out var salts);

                user.Id = Guid.NewGuid();
                user.Password = hashPassword;
                user.Salts = Convert.ToHexString(salts);

                await _repo.CreateAsync(user);

                return user;
            }
            else {
                throw new Exception("user already exists");
            }
        }

        public async Task<string> AuthenticateAsync(LoginUserDto dto)
        {
            var user = await _repo.GetByUsernameAsync(dto.Username);
            if (user == null || user.Status == Domain.Enums.UserStatus.Deleted)
            {
                throw new NonAuthenticateException();
            }
           
            //Verify 
            var verify = PasswordUtil.VerifyPassword(dto.Password, user.Password, Convert.FromHexString(user.Salts), _passwordSetting);
            _logger.Info($"Verify: {verify}");

            if (!verify)
            {
                throw new NonAuthenticateException();
            }

            var authenticatedUser = _mapper.Map<AuthenticatedUserModel>(user);

            return JwtUtil.CreateJwtToken(_jwtTokenSetting, authenticatedUser);
        }
        public async Task<IdentityUser> Getinformation(string username)
        {
            var user = await _repo.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new NonAuthenticateException();
            }
            return user;
        }
        
    }
}
