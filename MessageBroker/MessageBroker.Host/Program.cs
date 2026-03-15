using System;

namespace MessageBroker.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = ServiceProviderExtension.Configure();

            var host = new Host(provider);

            host.Start();

            Console.ReadLine();

            host.Stop();
        }
    }
}
