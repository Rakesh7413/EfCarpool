using System;
using System.Collections.Generic;

namespace Carpool.Data.Models
{
    public partial class PriceLimit
    {
        public int CarType { get; set; }
        public decimal Price { get; set; }
    }
}
