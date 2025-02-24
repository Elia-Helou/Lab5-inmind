using DDDProject.Application.Repositories; // Assuming the repository is here
using DDDProject.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using DDDProject.Infrastructure.Configurations;

namespace DDDProject.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository; 

        public EmailService(IConfiguration configuration, IStudentRepository studentRepository)
        {
            _configuration = configuration;
            _studentRepository = studentRepository;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var client = new SmtpClient(emailSettings.SmtpServer)
            {
                Port = emailSettings.SmtpPort,
                Credentials = new NetworkCredential(emailSettings.SmtpUsername, emailSettings.SmtpPassword),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(emailSettings.FromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(to);

            await client.SendMailAsync(message);
        }
        
        private async Task<List<Student>> GetStudentsToSendEmails()
        {
            var students = await _studentRepository.GetAllAsync(); // Assuming GetAllAsync() is a method in your repository

            return students.Where(student => !string.IsNullOrEmpty(student.Email)).ToList();
        }

        public async Task SendEmailsToStudentsAsync(string subject, string body)
        {
            var students = await GetStudentsToSendEmails();

            foreach (var student in students)
            {
                if (!string.IsNullOrEmpty(student.Email))
                {
                    await SendEmailAsync(student.Email, subject, body); 
                }
            }
        }
    }
}
