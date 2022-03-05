using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019EJ650_2019PA603.Models
{
    public class Productos
    {   [Key]
        public int Id { get; set; }
        public String Producto { get; set; }
        public double Precio { get; set; }
    }
}
