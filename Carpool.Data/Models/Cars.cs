using System;
using System.Collections.Generic;

namespace Carpool.Data.Models
{
    public partial class Cars
    {
        public string CarNo { get; set; }
        public string CarName { get; set; }
        public short CarType { get; set; }
        public int Capacity { get; set; }
        public int OwnerId { get; set; }
    }
}
