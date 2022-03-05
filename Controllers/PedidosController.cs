using _2019EJ650_2019PA603.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019EJ650_2019PA603.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ventasContext _context;
        public PedidosController(ventasContext miContexto)
        {
            this._context = miContexto;
        }

        [HttpGet]
        [Route("api/pedidos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoPedidos = (from e in _context.Pedidos
                                      join m in _context.Clientes on e.IdCliente equals m.Id

                                      select new
                                      {
                                          e.Id,
                                          e.IdCliente,
                                          e.FechaPedido
                                      }).OrderBy(m => m.Id);
                if (listadoPedidos.Count() > 0)
                {
                    return Ok(listadoPedidos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/pedidos/{Id}")]
        public IActionResult Get(int Id)
        {
            try
            {
                var xPedidos = (from e in _context.Pedidos
                                join m in _context.Clientes on e.IdCliente equals m.Id

                                select new
                                {
                                    e.Id,
                                    e.IdCliente,
                                    e.FechaPedido
                                }).FirstOrDefault();
                if (xPedidos != null)
                {
                    return Ok(xPedidos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/pedidos")]
        public IActionResult guardarPedido([FromBody] Pedidos pedidoNuevo)
        {
            try
            {
                _context.Pedidos.Add(pedidoNuevo);
                _context.SaveChanges();
                return Ok(pedidoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Pedidos")]
        public IActionResult updateEquipo([FromBody] Pedidos equipoAModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            Pedidos equipoExiste = (from e in _context.Pedidos
                                    where e.Id == equipoAModificar.Id
                                    select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                // Si no existe el registro retornar un NO ENCONTRADO
                return NotFound();
            }

            //Si se encuentra el registro, se alteran los campos a modificar
            equipoExiste.FechaPedido = equipoAModificar.FechaPedido;

            //Se envia el objeto a la base de datos.
            _context.Entry(equipoExiste).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(equipoExiste);
        }
    }
}
