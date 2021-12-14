using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Seguridad
{
    public class LoginModel
    {
        private string userName;

        [DisplayName("Correo")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener una longitud máxima de {1} caracteres")]

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;

        [DisplayName("Contraseña")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener una longitud máxima de {1} caracteres")]

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}