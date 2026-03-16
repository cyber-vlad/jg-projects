using MessageBroker.Common.Enums;
using MessageBroker.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MessageBroker.Common.Entities
{
    [DataContract]
    public class Topic : BaseEntity
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public TopicType Type { get; set; }

        [DataMember] public ICollection<string> QueuePaths { get; set; }

        [DataMember] public List<string> Descriptions { get; set; }
        [DataMember] public Dictionary<string, string> Tags { get; set; }
        [DataMember] public Tuple<int, string> Metadata { get; set; }

        public void Show()
        {
            Console.WriteLine($"Id: {Id}");

            Console.WriteLine($"CreatedAt: {CreatedAt}");
            Console.WriteLine($"UpdatedAt: {UpdatedAt}");

            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Type: {Type}");

            Console.WriteLine("QueuePaths:");
            foreach (var path in QueuePaths)
            {
                Console.WriteLine($" - {path}");
            }

            Console.WriteLine("Descriptions:");
            foreach (var desc in Descriptions)
            {
                Console.WriteLine($" - {desc}");
            }

            Console.WriteLine("Tags:");
            foreach (var tag in Tags)
            {
                Console.WriteLine($" - {tag.Key}: {tag.Value}");
            }

            Console.WriteLine("Metadata:");
            Console.WriteLine($" - Item1: {Metadata.Item1}");
            Console.WriteLine($" - Item2: {Metadata.Item2}");
        }
    }
}
