using System;

namespace EventBus.Messages
{
    public interface IOrderCreated
    {
        public int Id { get; set; }

        public String ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
