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

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Equipo> listadoEquipo = (from e in _EquiposContexto.Equipos select e).ToList();

            if (listadoEquipo.Count() == 0) { 
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
    }
}
