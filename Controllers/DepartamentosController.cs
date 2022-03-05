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
    public class DepartamentosController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public DepartamentosController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/departamentos/")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Departamentos> departamentosList = from de in _contexto.Departamentos
                                                               select de;
                if (departamentosList.Count() > 0)
                {
                    return Ok(departamentosList);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/departamentos/{idDepartamento}")]
        public IActionResult Get(int idDepartamento)
        {
            try
            {
                Departamentos departamento = (from de in _contexto.Departamentos
                                              where de.id == idDepartamento
                                              select de).FirstOrDefault();
                if (departamento != null)
                {
                    return Ok(departamento);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/departamentos/")]
        public IActionResult GuardarDepartamento([FromBody] Departamentos deptoNuevo)
        {
            try
            {
                _contexto.Add(deptoNuevo);
                _contexto.SaveChanges();
                return Ok(deptoNuevo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/departamentos/")]
        public IActionResult UpdateDepartamento([FromBody] Departamentos deptoAModificar)
        {
            try
            {
                Departamentos deptoExiste = (from de in _contexto.Departamentos
                                             where de.id == deptoAModificar.id
                                             select de).FirstOrDefault();
                if (deptoAModificar is null)
                {
                    return NotFound();
                }

                deptoExiste.departamento = deptoAModificar.departamento;

                _contexto.Entry(deptoExiste).State = EntityState.Modified;
                _contexto.SaveChanges();
                return Ok(deptoExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}

