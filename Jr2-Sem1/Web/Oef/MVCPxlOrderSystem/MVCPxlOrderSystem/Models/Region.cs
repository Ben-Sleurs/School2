using System;
using System.Collections.Generic;

namespace MVCPxlOrderSystem.Models
{
    public partial class Region
    {
        public Region()
        {
            Clients = new HashSet<Client>();
        }

        public int RegionId { get; set; }
        public string? RegionName { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
