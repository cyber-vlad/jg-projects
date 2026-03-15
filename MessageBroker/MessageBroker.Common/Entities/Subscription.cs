namespace MessageBroker.Common.Entities
{
    public class Subscription
    {
        public int SubscriberId { get; set; }
        public string Topic { get; set; }
        public string QueuePath { get; set; }
    }
}
