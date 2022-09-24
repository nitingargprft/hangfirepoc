using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangFire.Jobs.Models
{
    public class OrderLineItemModel
    {
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}