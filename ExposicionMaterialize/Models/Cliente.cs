using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExposicionMaterialize.Models
{
    public class Cliente
    {
        [Required]
        [Display(Name ="Identificación")]
        public string IdCliente { get; set; }

        [Required]
        public string Nombre { get; set; }

        [EmailAddress]
        public string Correo { get; set; }

        [Phone]
        public string Telefono { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public override string ToString()
        {
            return ToJsonString();
        }
    }
}