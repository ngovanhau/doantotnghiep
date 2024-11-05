using System.Data;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Databases;
using Common.Repositories;
using Dapper;
using Domain.Entities;
using Common.Application.CustomAttributes;
using Common.Services;
using Repositories;
using Utilities;
using System.Net.Mail;
using System.Net;


namespace Repositories
{
    [ScopedService]
    public class SendEmailRepository(IDbConnection connection) : SimpleCrudRepository<Problem, Guid>(connection)
    {
        private readonly string _smtpServer = "smtp.gmail.com"; 
        private readonly int _smtpPort = 587; 
        private readonly string _smtpUser = "vanhau141002@gmail.com"; 
        private readonly string _smtpPass = "qufxyzjzayofzgwd"; 
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true, // Bật SSL nếu SMTP server yêu cầu
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }
    }
}

