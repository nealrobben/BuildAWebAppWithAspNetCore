using Microsoft.Extensions.Logging;

namespace DutchTreat.Services
{
    public interface IMail
    {
        void SendMessage(string to, string subject, string body);
    }

    public class NullMailService : IMail
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(string to, string subject, string body)
        {
            //Log the message
            _logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}