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
    public class ProductosController : ControllerBase
    {
        private readonly ventasContext _context;

        public ProductosController(ventasContext miContexto)
        {
            this._context = miContexto;
        }

        [HttpGet]
        [Route("api/productos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoProductos = (from e in _context.Productos
                                        select new
                                        {
                                            e.Id,
                                            e.Producto,
                                            e.Precio
                                        }).OrderBy(m => m.Id);
                if (listadoProductos.Count() > 0)
                {
                    return Ok(listadoProductos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/productos/{Id}")]
        public IActionResult Get(int Id)
        {
            try
            {
                var xProductos = (from e in _context.Productos
                                  select new
                                  {
                                      e.Id,
                                      e.Producto,
                                      e.Precio
                                  }).FirstOrDefault();
                if (xProductos != null)
                {
                    return Ok(xProductos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/productos")]
        public IActionResult guardarProducto([FromBody] Productos productoNuevo)
        {
            try
            {
                _context.Productos.Add(productoNuevo);
                _context.SaveChanges();
                return Ok(productoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/productos")]
        public IActionResult updateEquipo([FromBody] Productos equipoAModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            Productos productoExiste = (from e in _context.Productos
                                        where e.Id == equipoAModificar.Id
                                        select e).FirstOrDefault();
            if (productoExiste is null)
            {
                // Si no existe el registro retornar un NO ENCONTRADO
                return NotFound();
            }

            //Si se encuentra el registro, se alteran los campos a modificar
            productoExiste.Producto = equipoAModificar.Producto;
            productoExiste.Precio = equipoAModificar.Precio;

            //Se envia el objeto a la base de datos.
            _context.Entry(productoExiste).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(productoExiste);
        }

    }
}
