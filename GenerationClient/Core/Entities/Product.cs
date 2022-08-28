using System;

namespace GenerationClient.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
