using System;

namespace OrderWebApi.Entities
{
    public class OrderDto
    {
        public String ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}