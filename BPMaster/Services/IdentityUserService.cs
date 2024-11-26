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
using BPMaster.Domains.Dtos;


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
        public async Task<InformationDto> Getinformation(string username)
        {
            var user = await _repo.GetByUsernameAsync(username);

            var infomation = _mapper.Map<InformationDto>(user);
            if (user == null)
            {
                throw new NonAuthenticateException();
            }
            return infomation;
        }
        public async Task ChangePasswordAsync(ChangePassWordDto dto)
        {   
            if(dto.OldPassword == null)
            {
                throw new NonAuthenticateException("The old password is not left blank");
            }
            if (dto.NewPassword == null) {
                throw new NonAuthenticateException("The new password is not left blank");
            }

            var user = await _repo.GetByUsernameAsync(dto.Username);
            if (user == null || user.Status == Domain.Enums.UserStatus.Deleted)
            {
                throw new NonAuthenticateException();
            }

            // Xác minh mật khẩu cũ
            var verifyOldPassword = PasswordUtil.VerifyPassword(dto.OldPassword, user.Password, Convert.FromHexString(user.Salts), _passwordSetting);
            if (!verifyOldPassword)
            {
                throw new Exception("Old password is incorrect");
            }

            // Hash mật khẩu mới và cập nhật
            var hashNewPassword = PasswordUtil.HashPBKDF2(dto.NewPassword, _passwordSetting, out var newSalts);
            user.Password = hashNewPassword;
            user.Salts = Convert.ToHexString(newSalts);

            await _repo.UpdateAsync(user);
        }

        public async Task<List<InformationDto>> GetAllUser()
        {
            var users = await _repo.GetAllUser();

            var result = _mapper.Map<List<InformationDto>>(users);

            return result;
        }
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _repo.GetByIDUser(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Status = UserStatus.Deleted;
            await _repo.UpdateAsync(user);
        }
        public async Task UpdateAvata(Guid id, string avata)
        {
            var user = await _repo.GetByIDUser(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _repo.Updateavata(id, avata);
        }
    }
}
