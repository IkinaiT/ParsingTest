using Flurl.Http;
using ParsingTest.Models;
using ParsingTest.Services.Interfaces;

namespace ParsingTest.Services.Runtime
{
    public class TimedService(IDataBaseService dataBaseService) : IHostedService, IDisposable
    {
        private Timer? _timer = null;
        private FlurlClient client = new("https://line32w.bk6bba-resources.com");
        private IDataBaseService _dbService = dataBaseService;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(async _ => await StartBackgroundTask(), null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public async Task StartBackgroundTask()
        {
            var result = await client.Request("events", "listBase")
                .AppendQueryParam("lang", "ru")
                .AppendQueryParam("scopeMarket", "1600")
                .GetAsync()
                .ReceiveJson<Result>();

            _dbService.LastResult = result;

            List<Factor> factors = [];

            foreach(var cf in result.CustomFactors)
            {
                factors.AddRange(cf.Factors);
            }

            await _dbService.SetData(result.Sports, result.Events, factors);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
