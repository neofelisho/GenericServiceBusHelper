# GenericServiceBusHelper
###### tags: `Azure` `WebJobs` `Service Bus` `Message Queue` `Generic` `ServiceBusTrigger` `TimerTrigger`

`Scenario` We need a generic service bus queue helper. Write enqueue method for each queue to avoid misusing.

`Testing`
- Unit test the object to message, and then deserilize the message to object.
- Use a real service bus to test the `ServiceBusTrigger` working correctly.
- Use a timer trigger to enqueue object, and monitor the reaction of `ServiceBusTrigger` is correct.