using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SensorData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SensorDataAPI.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(List<UserModel> users, List<ContactModel> contacts, string subject, string body)
        {
            try
            {
                var client = new SendGridClient(_emailSettings.SendGridApiKey);
                var from = new EmailAddress(_emailSettings.FromEmail, _emailSettings.FromName);

                // Cria a mensagem
                var msg = new SendGridMessage
                {
                    From = from,
                    Subject = subject,
                    PlainTextContent = body // Usa o conteúdo diretamente (sem template)
                };

                // Adiciona os usuários como destinatários
                foreach (var user in users)
                {
                    msg.AddTo(new EmailAddress(user.Email, user.Name));
                }

                // Adiciona os contatos de emergência como destinatários
                foreach (var contact in contacts)
                {
                    msg.AddTo(new EmailAddress(contact.Email, contact.Name));
                }

                // Envia o e-mail
                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                    response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception($"Erro ao enviar e-mail: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail: " + ex.Message);
            }
        }
    }
}