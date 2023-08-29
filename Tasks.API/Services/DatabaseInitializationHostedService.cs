using Tasks.Data.Interfaces;
using Tasks.Data.Services;

namespace Tasks.API.Services
{
    public class DatabaseInitializationHostedService : IHostedService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public DatabaseInitializationHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = this.serviceScopeFactory.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
                await initializer.InitializeAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
