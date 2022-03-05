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
    public class DetallePedidosController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public DetallePedidosController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/detalles_pedidos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoDetalles = (from dp in _contexto.DetallePedidos
                                       join pe in _contexto.Pedidos on dp.IdPedido equals pe.Id
                                       join po in _contexto.Productos on dp.IdProducto equals po.Id
                                       join c in _contexto.Clientes on pe.IdCliente equals c.Id
                                       select new
                                       {
                                           dp.Id,
                                           dp.IdPedido,
                                           cliente = c.Nombre,
                                           fecha = pe.FechaPedido,
                                           dp.IdProducto,
                                           producto = po.Producto,
                                           precio = po.Precio,
                                           dp.Cantidad
                                       }).OrderBy(pe => pe.IdPedido);
                if (listadoDetalles.Count() > 0)
                {
                    return Ok(listadoDetalles);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/detalles_pedidos/{idDetalle}")]
        public IActionResult Get(int idDetalle)
        {
            try
            {
                var unDetalle = (from dp in _contexto.DetallePedidos
                                 join pe in _contexto.Pedidos on dp.IdPedido equals pe.Id
                                 join po in _contexto.Productos on dp.IdProducto equals po.Id
                                 join c in _contexto.Clientes on pe.IdCliente equals c.Id
                                 where dp.Id == idDetalle
                                 select new
                                 {
                                     dp.Id,
                                     dp.IdPedido,
                                     cliente = c.Nombre,
                                     fecha = pe.FechaPedido,
                                     dp.IdProducto,
                                     producto = po.Producto,
                                     precio = po.Precio,
                                     dp.Cantidad
                                 }).FirstOrDefault();
                if (unDetalle != null)
                {
                    return Ok(unDetalle);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/detalles_pedidos")]
        public IActionResult guardarCliente([FromBody] DetallePedidos detalleNuevo)
        {
            try
            {
                _contexto.DetallePedidos.Add(detalleNuevo);
                _contexto.SaveChanges();
                return Ok(detalleNuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/detalles_pedidos")]
        public IActionResult updateDetalle([FromBody] DetallePedidos detalleAModificar)
        {
            try
            {
                DetallePedidos detalleExiste = (from dp in _contexto.DetallePedidos
                                                where dp.Id == detalleAModificar.Id
                                                select dp).FirstOrDefault();

                if (detalleExiste is null)
                {
                    return NotFound();
                }

                detalleExiste.Cantidad = detalleAModificar.Cantidad;
                detalleExiste.IdProducto = detalleAModificar.IdProducto;

                _contexto.Entry(detalleExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(detalleExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
