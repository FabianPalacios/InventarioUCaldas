using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Seguridad
{
    public class PasswordResetModel
    {
        private string userName;

        [DisplayName("Correo")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener una longitud máxima de {1} caracteres")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}