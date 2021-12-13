using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Seguridad
{
    public class ChangePasswordModel
    {

        private string password;

        [DisplayName("Contraseña actual")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener una longitud máxima de {1} caracteres")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string newPassword;

        [DisplayName("Nueva contraseña")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener una longitud máxima de {1} caracteres")]
        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; }
        }
    }
}