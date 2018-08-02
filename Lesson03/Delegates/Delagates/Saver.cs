using Microsoft.Extensions.Logging;

namespace Delagates
{
    public class Saver
    {
        private readonly ILogger<Saver> _logger;

        public Saver()
        {
            var factory = new LoggerFactory()
                .AddConsole();
            _logger = factory.CreateLogger<Saver>();
        }
        public void Save(object input)
        {
            _logger.LogInformation(input.ToString());
        }
    }
}
