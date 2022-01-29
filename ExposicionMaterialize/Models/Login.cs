using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExposicionMaterialize.Models
{
    public class Login
    {

        [Required]
        public string Usuario { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

    }
}