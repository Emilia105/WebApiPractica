using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosEquipoController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public EstadosEquipoController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<EstadosEquipo> listadoEstadosEquipo = (from e in _EquiposContexto.EstadosEquipos select e).ToList();

            if (listadoEstadosEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEstadosEquipo);
        }

        //endpoint filtro por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            EstadosEquipo? EstadosEquipo = (from e in _EquiposContexto.EstadosEquipos where e.IdEstadosEquipo == id select e).FirstOrDefault();
            if (EstadosEquipo == null)
            {
                return NotFound();
            }
            return Ok(EstadosEquipo);
        }
        //endpoint filtro por descripción
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            EstadosEquipo? EstadosEquipo = (from e in _EquiposContexto.EstadosEquipos where e.Descripcion.Contains(filtro) select e).FirstOrDefault();
            if (EstadosEquipo == null)
            {
                return NotFound();
            }
            return Ok(EstadosEquipo);
        }
        //Post :D
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEstadoEquipo([FromBody] EstadosEquipo EstadosEquipo)
        {
            try
            {
                _EquiposContexto.EstadosEquipos.Add(EstadosEquipo);
                _EquiposContexto.SaveChanges();
                return Ok(EstadosEquipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEstadoEquipo(int id, [FromBody] EstadosEquipo estadosModificar)
        {
            //obtencion de registro original
            EstadosEquipo? estadosActual = (from e in _EquiposContexto.EstadosEquipos
                                    where e.IdEstadosEquipo == id
                                    select e).FirstOrDefault();

            //verificacion de existencia del registro segun ID
            if (estadosActual == null)
            {
                return NotFound();
            }
            //Alteración de los campos
            estadosActual.Descripcion = estadosModificar.Descripcion;
            estadosActual.Estado = estadosModificar.Estado;

            //registro marcado como modificado en el contexto y se envia a la modificacion a la bd
            _EquiposContexto.Entry(estadosActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(estadosModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEstadoEquipo(int id)
        {
            //Obtención de registro
            EstadosEquipo? estadosEquipo = (from e in _EquiposContexto.EstadosEquipos
                              where e.IdEstadosEquipo == id
                              select e).FirstOrDefault();
            //verificacion de la existencia del registro
            if (estadosEquipo == null)
            {
                return NotFound();
            }
            //Eliminación del registro
            _EquiposContexto.EstadosEquipos.Attach(estadosEquipo);
            _EquiposContexto.EstadosEquipos.Remove(estadosEquipo);
            _EquiposContexto.SaveChanges();

            return Ok(estadosEquipo);
        }

    }
}
