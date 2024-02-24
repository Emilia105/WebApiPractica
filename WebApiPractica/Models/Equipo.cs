using System;
using System.Collections.Generic;

namespace WebApiPractica.Models;

public partial class Equipo
{
    public int IdEquipos { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? TipoEquipoId { get; set; }

    public int? MarcaId { get; set; }

    public string? Modelo { get; set; }

    public int? AnioCompra { get; set; }

    public decimal? Costo { get; set; }

    public int? VidaUtil { get; set; }

    public int? EstadoEquipoId { get; set; }

    public string? Estado { get; set; }
}
