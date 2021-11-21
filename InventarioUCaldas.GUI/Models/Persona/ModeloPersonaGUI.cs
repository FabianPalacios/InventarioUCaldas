using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Persona
{
    public class ModeloPersonaGUI
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string primer_nombre;
        [DisplayName("Primer Nombre")]
        public string Primer_Nombre
        {
            get { return primer_nombre; }
            set { primer_nombre = value; }
        }

        private string otros_nombres;

        [DisplayName("Otros Nombres")]
        public string Otros_Nombres
        {
            get { return otros_nombres; }
            set { otros_nombres = value; }
        }

        private string primer_apellido;
        [DisplayName("Primer Apellido")]
        public string Primer_Apellido
        {
            get { return primer_apellido; }
            set { primer_apellido = value; }
        }

        private string segundo_apellido;
        [DisplayName("Segundo Nombre")]
        public string Segundo_Apellido
        {
            get { return segundo_apellido; }
            set { segundo_apellido = value; }
        }

        private string documento;
        [DisplayName("Cédula")]
        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }

        private string celular;

        [DisplayName("Teléfono")]
        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }

        private string correo;

        [DisplayName("Correo")]
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }
    }
}