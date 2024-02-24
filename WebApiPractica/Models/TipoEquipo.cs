using System;
using System.Collections.Generic;

namespace WebApiPractica.Models;

public partial class TipoEquipo
{
    public int IdTipoEquipo { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }
}
