using System;
using System.Collections.Generic;

namespace MVCPxlOrderSystem.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ClientId { get; set; }

        public virtual Client? Client { get; set; }
    }
}
