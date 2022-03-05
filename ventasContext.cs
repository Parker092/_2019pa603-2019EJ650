using _2019EJ650_2019PA603.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019EJ650_2019PA603
{
    public class ventasContext : DbContext
    {
        public ventasContext(DbContextOptions<ventasContext> options) : base(options)
        {

        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<DetallePedidos> DetallePedidos { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Productos> Productos { get; set; }
    }
}
