using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class TravellCharges
    {
        public List<CarItem> Cars { get; set; }
    }
    public class CarItem
    {
        public int Id { get; set; }
        public string CarType { get;set; }
        public decimal MaxCost { get;set; }
    }
}
