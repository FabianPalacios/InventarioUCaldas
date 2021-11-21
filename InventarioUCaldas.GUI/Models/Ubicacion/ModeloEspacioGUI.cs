using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Ubicacion
{
    public class ModeloEspacioGUI
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private int idPiso;

        public int IdPiso
        {
            get { return idPiso; }
            set { idPiso = value; }
        }

        private string nombrePiso;
        [DisplayName("Piso")]
        public string NombrePiso
        {
            get { return nombrePiso; }
            set { nombrePiso = value; }
        }
    }
}