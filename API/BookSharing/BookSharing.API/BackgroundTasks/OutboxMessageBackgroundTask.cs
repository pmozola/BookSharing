using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookSharing.API.BackgroundTasks
{
    public class OutboxMessageBackgroundTask : BackgroundService
    {
        private readonly ILogger<OutboxMessageBackgroundTask> _logger;
        private readonly IServiceProvider _services;

        public OutboxMessageBackgroundTask(ILogger<OutboxMessageBackgroundTask> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                try
                {
                    using var scope = _services.CreateScope();

                    var context = scope.ServiceProvider.GetService<BookSharingDbContext>();
                    var publisher = scope.ServiceProvider.GetService<IPublisher>();
                    var messages = context.OutboxMessages.ToList();

                    foreach (var message in messages)
                    {
                        var type = Assembly.GetAssembly(typeof(Book)).GetType(message.Type);
                        var notification = JsonConvert.DeserializeObject(message.Data, type);

                        await publisher.Publish((INotification)notification, stoppingToken);
                    }

                    context.OutboxMessages.RemoveRange(messages);
                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Cannot process messages from outbox : {ex}");
                }
            }
        }
    }
}
