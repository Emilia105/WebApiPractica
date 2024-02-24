using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public MarcasController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Marca> listadoMarcas = (from e in _EquiposContexto.Marcas select e).ToList();

            if (listadoMarcas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoMarcas);
        }

        //endpoint filtro por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Marca? marca = (from e in _EquiposContexto.Marcas where e.IdMarcas == id select e).FirstOrDefault();
            if (marca == null)
            {
                return NotFound();
            }
            return Ok(marca);
        }
        //endpoint filtro por nombre
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByName(string filtro)
        {
            Marca? marca = (from e in _EquiposContexto.Marcas where e.NombreMarca.Contains(filtro) select e).FirstOrDefault();
            if (marca == null)
            {
                return NotFound();
            }
            return Ok(marca);
        }
        //Post :D
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarMarca([FromBody] Marca marca)
        {
            try
            {
                _EquiposContexto.Marcas.Add(marca);
                _EquiposContexto.SaveChanges();
                return Ok(marca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarMarca(int id, [FromBody] Marca marcaModificar)
        {
            //obtencion de registro original
            Marca? MarcasActual = (from e in _EquiposContexto.Marcas
                                            where e.IdMarcas == id
                                            select e).FirstOrDefault();

            //verificacion de existencia del registro segun ID
            if (MarcasActual == null)
            {
                return NotFound();
            }
            //Alteración de los campos
            MarcasActual.NombreMarca = marcaModificar.NombreMarca;
            MarcasActual.Estados = marcaModificar.Estados;

            //registro marcado como modificado en el contexto y se envia a la modificacion a la bd
            _EquiposContexto.Entry(MarcasActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(marcaModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarMarca(int id)
        {
            //Obtención de registro
            Marca? marca = (from e in _EquiposContexto.Marcas
                                            where e.IdMarcas == id
                                            select e).FirstOrDefault();
            //verificacion de la existencia del registro
            if (marca == null)
            {
                return NotFound();
            }
            //Eliminación del registro
            _EquiposContexto.Marcas.Attach(marca);
            _EquiposContexto.Marcas.Remove(marca);
            _EquiposContexto.SaveChanges();

            return Ok(marca);
        }
    }
}
