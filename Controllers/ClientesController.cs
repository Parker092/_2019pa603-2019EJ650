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
    public class ClientesController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public ClientesController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/clientes/")]
        public IActionResult Get()
        {
            try
            {
                var listadoClientes = (from c in _contexto.Clientes
                                       join d in _contexto.Departamentos on c.IdDepartamento equals d.id
                                       select new
                                       {
                                           c.Id,
                                           c.Nombre,
                                           c.IdDepartamento,
                                           departamento = d.departamento,
                                           c.FechaNac
                                       }).OrderBy(c => c.Nombre);
                if (listadoClientes.Count() > 0)
                {
                    return Ok(listadoClientes);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/clientes/{idCliente}")]
        public IActionResult Get(int idCliente)
        {
            try
            {
                var unEquipo = (from c in _contexto.Clientes
                                join d in _contexto.Departamentos on c.IdDepartamento equals d.id
                                where c.Id == idCliente
                                select new
                                {
                                    c.Id,
                                    c.Nombre,
                                    c.IdDepartamento,
                                    departamento = d.departamento,
                                    c.FechaNac
                                }).FirstOrDefault();
                if (unEquipo != null)
                {
                    return Ok(unEquipo);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/clientes")]
        public IActionResult guardarCliente([FromBody] Clientes clienteNuevo)
        {
            try
            {
                _contexto.Clientes.Add(clienteNuevo);
                _contexto.SaveChanges();
                return Ok(clienteNuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/clientes")]
        public IActionResult updateCliente([FromBody] Clientes clienteAModificar)
        {
            try
            {
                Clientes clienteExiste = (from c in _contexto.Clientes
                                          where c.Id == clienteAModificar.Id
                                          select c).FirstOrDefault();

                if (clienteExiste is null)
                {
                    return NotFound();
                }

                clienteExiste.Nombre = clienteAModificar.Nombre;
                clienteExiste.IdDepartamento = clienteAModificar.IdDepartamento;

                _contexto.Entry(clienteExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(clienteExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
