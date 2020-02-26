using System;
using System.Collections.Generic;

namespace Carpool.Data.Models
{
    public partial class RouteInformations
    {
        public int Id { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
    }
}
