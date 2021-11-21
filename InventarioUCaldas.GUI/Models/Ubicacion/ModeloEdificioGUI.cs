using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Ubicacion
{
    public class ModeloEdificioGUI
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

        private string bloque;

        public string Bloque
        {
            get { return bloque; }
            set { bloque = value; }
        }

        private int idSede;

        public int IdSede
        {
            get { return idSede; }
            set { idSede = value; }
        }

        private string nombreSede;

        [DisplayName("Sede")]
        public string NombreSede
        {
            get { return nombreSede; }
            set { nombreSede = value; }
        }
    }
}