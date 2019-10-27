using System;
using System.Collections.Generic;

namespace testing.core
{
    public class OrderItem
    {
         public int Id { get; set; }
         public string NmBarang { get; set; }
         public List<Order> OrderItemList { get; set; }
    }
}
