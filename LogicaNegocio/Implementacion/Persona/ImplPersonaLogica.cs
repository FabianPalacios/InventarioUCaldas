using AccesoDeDatos.DbModel.Persona;
using AccesoDeDatos.Implementacion.Persona;
using LogicaNegocio.DTO.Persona;
using LogicaNegocio.Mapeadores.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Implementacion.Persona
{
    public class ImplPersonaLogica
    {
        private ImplPersonaDatos accesoDatos;

        public ImplPersonaLogica()
        {
            this.accesoDatos = new ImplPersonaDatos();
        }

        /// <summary>
        /// Listar Registros
        /// </summary>
        /// <param name="filtro">filtro de busqueda</param>
        /// <param name="numPagina">Pagina Actual</param>
        /// <param name="registrosPorPagina">Cantidad de registros a monstrar por pagina</param>
        /// <param name="totalRegistros">Total de registros en base de datos</param>
        /// <returns>Listado de registros para mostrar en la página actual que coincidan con el filtro</returns>
        public IEnumerable<PersonaDTO> ListarRegistros(String filtro, int numPagina, int registrosPorPagina, out int totalRegistros)
        {
            var listado = this.accesoDatos.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorPersonaLogica mapeador = new MapeadorPersonaLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public PersonaDTO BuscarRegistro(int id)
        {
            var registro = this.accesoDatos.BuscarRegistro(id);
            MapeadorPersonaLogica mapeador = new MapeadorPersonaLogica();
            return mapeador.MapearTipo1Tipo2(registro);
        }
        public PersonaDTO BuscarCorreoPersona(string documento)
        {
            var registro = this.accesoDatos.BuscarCorreoPersona(documento);
            MapeadorPersonaLogica mapeador = new MapeadorPersonaLogica();
            return mapeador.MapearTipo1Tipo2(registro);
        }

        public Boolean GuardarRegistro(PersonaDTO registro)
        {
            MapeadorPersonaLogica mapeador = new MapeadorPersonaLogica();
            PersonaDbModel reg = mapeador.MapearTipo2Tipo1(registro);
            Boolean res = this.accesoDatos.GuardarRegistro(reg);
            return res;
        }

        public Boolean EditarRegistro(PersonaDTO registro)
        {
            MapeadorPersonaLogica mapeador = new MapeadorPersonaLogica();
            PersonaDbModel reg = mapeador.MapearTipo2Tipo1(registro);
            Boolean res = this.accesoDatos.EditarRegistro(reg);
            return res;
        }

        public Boolean EliminarRegistro(int id)
        {
            Boolean res = this.accesoDatos.EliminarRegistro(id);
            return res;
        }
    }
}
