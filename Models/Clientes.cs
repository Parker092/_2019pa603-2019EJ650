using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019EJ650_2019PA603.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }
        public int IdDepartamento { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaNac { get; set; }

    }
}
