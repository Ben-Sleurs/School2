using System;
using System.Collections.Generic;

namespace MVCPxlOrderSystem.Models
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int ClientId { get; set; }
        public int RegionId { get; set; }
        public string? ClientName { get; set; }

        public virtual Region Region { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
