using AccesoDeDatos.DbModel.Ubicacion;
using AccesoDeDatos.Implementacion.Ubicacion;
using LogicaNegocio.DTO.Ubicacion;
using LogicaNegocio.Mapeadores.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Implementacion.Ubicacion
{
    public class ImplEdificioLogica
    {
        private ImplEdificioDatos accesoDatos;

        public ImplEdificioLogica()
        {
            this.accesoDatos = new ImplEdificioDatos();
        }

        /// <summary>
        /// Listar Registros
        /// </summary>
        /// <param name="filtro">filtro de busqueda</param>
        /// <param name="numPagina">Pagina Actual</param>
        /// <param name="registrosPorPagina">Cantidad de registros a monstrar por pagina</param>
        /// <param name="totalRegistros">Total de registros en base de datos</param>
        /// <returns>Listado de registros para mostrar en la página actual que coincidan con el filtro</returns>
        public IEnumerable<EdificioDTO> ListarRegistros(String filtro, int numPagina, int registrosPorPagina, out int totalRegistros)
        {
            var listado = this.accesoDatos.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorEdificioLogica mapeador = new MapeadorEdificioLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }
        public IEnumerable<SedeDTO> ListarRegistrosSede()
        {
            var listado = this.accesoDatos.ListarRegistrosSede();
            MapeadorSedeLogica mapeador = new MapeadorSedeLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public EdificioDTO BuscarRegistro(int id)
        {
            var registro = this.accesoDatos.BuscarRegistro(id);
            MapeadorEdificioLogica mapeador = new MapeadorEdificioLogica();
            return mapeador.MapearTipo1Tipo2(registro);
        }

        public Boolean GuardarRegistro(EdificioDTO registro)
        {
            MapeadorEdificioLogica mapeador = new MapeadorEdificioLogica();
            EdificioDbModel reg = mapeador.MapearTipo2Tipo1(registro);
            Boolean res = this.accesoDatos.GuardarRegistro(reg);
            return res;
        }

        public Boolean EditarRegistro(EdificioDTO registro)
        {
            MapeadorEdificioLogica mapeador = new MapeadorEdificioLogica();
            EdificioDbModel reg = mapeador.MapearTipo2Tipo1(registro);
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
