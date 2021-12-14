using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Seguridad
{
    public class RoleModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        [DisplayName("Nombre")]
        [Required()]
        [MaxLength(100, ErrorMessage = "El campo {0} puede tener una longitud máxima de {1} caracteres")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private bool removed;

        [DisplayName("Eliminado")]
        public bool Removed
        {
            get { return removed; }
            set { removed = value; }
        }

        private string description;

        [DisplayName("Descripción")]
        [MaxLength(200, ErrorMessage = "El campo {0} puede tener una longitud máxima de {1} caracteres")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private bool isSelectedByUser;

        public bool IsSelectedByUser
        {
            get { return isSelectedByUser; }
            set { isSelectedByUser = value; }
        }
    }
}