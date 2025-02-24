using Hangfire;
using DDDProject.Infrastructure.Services;

namespace DDDProject.Infrastructure.Jobs
{
    public class EmailNotificationJob
    {
        private readonly EmailService _emailService;

        public EmailNotificationJob(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task ExecuteAsync()
        {
            string subject = "Important Notification";
            string body = "This is a notification about your recent activity.";

            await _emailService.SendEmailsToStudentsAsync(subject, body);
        }
    }
}