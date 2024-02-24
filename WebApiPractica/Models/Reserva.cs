using System;
using System.Collections.Generic;

namespace WebApiPractica.Models;

public partial class Reserva
{
    public int ReservaId { get; set; }

    public int? EquipoId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaSalida { get; set; }

    public DateTime? HoraSalida { get; set; }

    public int? TiempoReserva { get; set; }

    public int? EstadoReservaId { get; set; }

    public DateTime? FechaRetorno { get; set; }

    public DateTime? HoraRetorno { get; set; }
}
