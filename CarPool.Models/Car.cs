using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Car
    {
        [Key]
        [Required]
        public string CarNo { get; set; }

        public string CarName { get; set; }

        public CarType CarType { get; set; }

        [Required]
        [Range(4,8,ErrorMessage ="Car capacity should lies between 4 and 8")]
        public int Capacity { get; set; }

        [Required]
        public int OwnerId { get; set; }

    }
}
