using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewsDemo
{
    public sealed class MiniFigure
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Race { get; set; }
        public override string ToString()
        {
            return this.Name + " is a " + this.Race;
        }
    }
}
