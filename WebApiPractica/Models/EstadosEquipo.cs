using System;
using System.Collections.Generic;

namespace WebApiPractica.Models;

public partial class EstadosEquipo
{
    public int IdEstadosEquipo { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }
}
