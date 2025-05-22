using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Shared.Events;

namespace WorkshopHub.Domain.Consumers
{
    public sealed class FanoutEventConsumer : IConsumer<FanoutDomainEvent>
    {
        private readonly ILogger<FanoutEventConsumer> _logger;

        public FanoutEventConsumer(ILogger<FanoutEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<FanoutDomainEvent> context)
        {
            _logger.LogInformation("FanoutDomainEventConsumer: {FanoutDomainEvent}", context.Message);
            return Task.CompletedTask;
        }
    }
}
