using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO.Ubicacion
{
    public class EspacioDTO
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

        public string NombrePiso
        {
            get { return nombrePiso; }
            set { nombrePiso = value; }
        }
    }
}
