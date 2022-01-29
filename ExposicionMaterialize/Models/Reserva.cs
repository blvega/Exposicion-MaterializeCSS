using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExposicionMaterialize.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; }

        [Required]
        [Display(Name = "Identificación")]
        public string IdCliente { get; set; }

        [Required]
        [Display(Name = "Fecha de ingreso")]
        public System.DateTime FechaIngreso { get; set; }

        [Required]
        [Display(Name = "Fecha de salida")]
        public System.DateTime FechaSalida { get; set; }

        [Required]
        public string Destino { get; set; }

        [Required]
        [Display(Name = "Número de personas")]
        public int NumeroPersonas { get; set; }
    }
}