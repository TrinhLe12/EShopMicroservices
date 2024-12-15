namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, IFeatureManager featureManager,ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent> //Inherit INotificationHandler to handle notificaton (domain event) sent from Infra layer by MediatR
    {
        //Consume Domain event
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

            //Check if feature is enabled before running
            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();

                //Publish Integration event to be handled by different micro services
                //TODO: Define other microservices to handle this integration event
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
        }
    }
}
