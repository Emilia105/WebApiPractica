using Microsoft.AspNetCore.Http;
using WebApiPractica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public equiposController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }
        //endpoint listado de todos los equipos
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Equipo> listadoEquipos = (from e in _EquiposContexto.Equipos select e).ToList();

            if (listadoEquipos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipos);
        }

        //endpoint filtro por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Equipo? equipo = (from e in _EquiposContexto.Equipos where e.IdEquipos == id select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        //endpoint filtro por descripción
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            Equipo? equipo = (from e in _EquiposContexto.Equipos where e.Descripcion.Contains(filtro) select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        //Post :D
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] Equipo equipo) 
        {
            try 
            {
                _EquiposContexto.Equipos.Add(equipo);
                _EquiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Equipo equipoModificar)
        {
            //obtencion de registro original
            Equipo? equipoActual = (from e in _EquiposContexto.Equipos
                                    where e.IdEquipos == id select e).FirstOrDefault();

            //verificacion de existencia del registro segun ID
            if(equipoActual == null)
            {
                return NotFound();
            }
            //Alteración de los campos
            equipoActual.Nombre = equipoModificar.Nombre;
            equipoActual.Descripcion = equipoModificar.Descripcion;
            equipoActual.MarcaId = equipoModificar.MarcaId;
            equipoActual.TipoEquipoId = equipoModificar.TipoEquipoId;
            equipoActual.AnioCompra = equipoModificar.AnioCompra;
            equipoActual.Costo = equipoModificar.Costo;
            //registro marcado como modificado en el contexto y se envia a la modificacion a la bd
            _EquiposContexto.Entry(equipoActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(equipoModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            //Obtención de registro
            Equipo? equipo = (from e in _EquiposContexto.Equipos
                              where e.IdEquipos == id
                              select e).FirstOrDefault();
            //verificacion de la existencia del registro
            if (equipo == null) 
            { 
                return NotFound();
            }
            //Eliminación del registro
            _EquiposContexto.Equipos.Attach(equipo);
            _EquiposContexto.Equipos.Remove(equipo);
            _EquiposContexto.SaveChanges();

            return Ok(equipo);
        }

    }
}
