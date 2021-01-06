using Core.Entities.Email;
using Core.Interfaces.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Email
{
    public sealed class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _mailSender;
        private readonly string _mailerName;
        private readonly string _activationMailSubject;
        private readonly string _baseUrl;
        private readonly string _systemUserActivateRoute;

        public EmailService(string apiKey, string mailSender, string mailerName, string activationMailSubject, string baseUrl , string systemUserActivateRoute)
        {
            _apiKey = apiKey;
            _mailSender = mailSender;
            _mailerName = mailerName;
            _activationMailSubject = activationMailSubject;
            _baseUrl = baseUrl;
            _systemUserActivateRoute = systemUserActivateRoute;
        }

        async Task IEmailService.SendActivationEmail(SystemUserActivationRequest systemUserActivationRequest)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_mailSender, _mailerName);
            string subject = _activationMailSubject;
            var to = new EmailAddress(systemUserActivationRequest.Email);
            string template = await ReadHtmlTemplateForMessage("ActivationEmail.html");
            string htmlContent = BuildHtmlActivationMessage(template , systemUserActivationRequest);

            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, htmlContent);
            Response response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception("No se pudo enviar el correo de activacion");
            }
        }

        private string BuildHtmlActivationMessage(string template, SystemUserActivationRequest systemUserActivationRequest)
        {
            string activateSystemUserUrl = BuildSystemUserActivationUrl(systemUserActivationRequest);
            string activationMessage = template.Replace("<activationLink>", activateSystemUserUrl);

            return activationMessage;
        }

        private string BuildSystemUserActivationUrl(SystemUserActivationRequest systemUserActivationRequest)
        {
            string activateSystemUserUrl = $"{_baseUrl}{_systemUserActivateRoute}";

            activateSystemUserUrl = activateSystemUserUrl.Replace("emailParam", systemUserActivationRequest.Email);
            activateSystemUserUrl = activateSystemUserUrl.Replace("encriptedUsernameParam", systemUserActivationRequest.EncriptedUsername);

            return activateSystemUserUrl;
        }

        private async Task<string> ReadHtmlTemplateForMessage(string filePath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates", filePath);
            string template = await File.ReadAllTextAsync(path);

            return template;
        }
    }
}
