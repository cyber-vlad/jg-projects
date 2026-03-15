namespace MessageBroker.Common.Consts
{
    public class BrokerConfig
    {
        public const string PublisherBaseUrl = "http://localhost:7001/publisher/";
        public const string SubscriberBaseUrl = "http://localhost:7002/subscriber/";

        public const string PublisherServiceName = "PublisherService";
        public const string SubscriberServiceName = "SubscriberService";
    }
}
