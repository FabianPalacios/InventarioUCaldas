using AccesoDeDatos.DbModel.Producto;
using AccesoDeDatos.Implementacion.Producto;
using LogicaNegocio.DTO.Producto;
using LogicaNegocio.Mapeadores.Producto;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Implementacion.Producto
{
    public class ImplTipoProductoLogica
    {
        private ImplTipoProductoDatos accesoDatos;

        public ImplTipoProductoLogica()
        {
            this.accesoDatos = new ImplTipoProductoDatos();
        }

        /// <summary>
        /// Listar Registros
        /// </summary>
        /// <param name="filtro">filtro de busqueda</param>
        /// <param name="numPagina">Pagina Actual</param>
        /// <param name="registrosPorPagina">Cantidad de registros a monstrar por pagina</param>
        /// <param name="totalRegistros">Total de registros en base de datos</param>
        /// <returns>Listado de registros para mostrar en la página actual que coincidan con el filtro</returns>
        public IEnumerable<TipoProductoDTO> ListarRegistros(String filtro, int numPagina, int registrosPorPagina, out int totalRegistros)
        {
            var listado = this.accesoDatos.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorTipoProductoLogica mapeador = new MapeadorTipoProductoLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public TipoProductoDTO BuscarRegistro(int id)
        {
            var registro = this.accesoDatos.BuscarRegistro(id);
            MapeadorTipoProductoLogica mapeador = new MapeadorTipoProductoLogica();
            return mapeador.MapearTipo1Tipo2(registro);
        }

        public Boolean GuardarRegistro(TipoProductoDTO registro)
        {
            MapeadorTipoProductoLogica mapeador = new MapeadorTipoProductoLogica();
            TipoProductoDbModel reg = mapeador.MapearTipo2Tipo1(registro);
            Boolean res = this.accesoDatos.GuardarRegistro(reg);
            return res;
        }

        public Boolean EditarRegistro(TipoProductoDTO registro)
        {
            MapeadorTipoProductoLogica mapeador = new MapeadorTipoProductoLogica();
            TipoProductoDbModel reg = mapeador.MapearTipo2Tipo1(registro);
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
