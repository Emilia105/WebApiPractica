using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public TipoController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<TipoEquipo> listadoTipos = (from e in _EquiposContexto.TipoEquipos select e).ToList();

            if (listadoTipos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoTipos);
        }

        //endpoint filtro por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            TipoEquipo? tipos = (from e in _EquiposContexto.TipoEquipos where e.IdTipoEquipo == id select e).FirstOrDefault();
            if (tipos == null)
            {
                return NotFound();
            }
            return Ok(tipos);
        }
        //endpoint filtro por nombre
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            TipoEquipo? tipos = (from e in _EquiposContexto.TipoEquipos where e.Descripcion.Contains(filtro) select e).FirstOrDefault();
            if (tipos == null)
            {
                return NotFound();
            }
            return Ok(tipos);
        }
        //Post :D
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarTipo([FromBody] TipoEquipo tipos)
        {
            try
            {
                _EquiposContexto.TipoEquipos.Add(tipos);
                _EquiposContexto.SaveChanges();
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarTipo(int id, [FromBody] TipoEquipo tipoModificar)
        {
            //obtencion de registro original
            TipoEquipo? tipoActual = (from e in _EquiposContexto.TipoEquipos
                                   where e.IdTipoEquipo == id
                                   select e).FirstOrDefault();

            //verificacion de existencia del registro segun ID
            if (tipoActual == null)
            {
                return NotFound();
            }
            //Alteración de los campos
            tipoActual.Descripcion = tipoModificar.Descripcion;
            tipoActual.Estado = tipoModificar.Estado;

            //registro marcado como modificado en el contexto y se envia a la modificacion a la bd
            _EquiposContexto.Entry(tipoActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(tipoModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarTipo(int id)
        {
            //Obtención de registro
            TipoEquipo? tipo = (from e in _EquiposContexto.TipoEquipos
                            where e.IdTipoEquipo == id
                            select e).FirstOrDefault();
            //verificacion de la existencia del registro
            if (tipo == null)
            {
                return NotFound();
            }
            //Eliminación del registro
            _EquiposContexto.TipoEquipos.Attach(tipo);
            _EquiposContexto.TipoEquipos.Remove(tipo);
            _EquiposContexto.SaveChanges();

            return Ok(tipo);
        }
    }
}
