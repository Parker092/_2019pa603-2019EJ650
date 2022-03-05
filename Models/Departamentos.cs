using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019EJ650_2019PA603.Models
{
    public class Departamentos { 

        [Key]
        public int id { get; set; }
        public String departamento { get; set; }

    }
}
