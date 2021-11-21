using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO.Ubicacion
{
    public class PisoDTO
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

        private int idEdificio;

        public int IdEdificio
        {
            get { return idEdificio; }
            set { idEdificio = value; }
        }

        private string nombreEdificio;

        public string NombreEdificio
        {
            get { return nombreEdificio; }
            set { nombreEdificio = value; }
        }
    }
}
