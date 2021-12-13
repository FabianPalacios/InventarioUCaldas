﻿using AccesoDeDatos.DbModel.Parametros;
using AccesoDeDatos.Implementacion.Parametros;
using LogicaNegocio.DTO.Parametros;
using LogicaNegocio.Mapeadores.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Implementacion.Parametros
{
    public class ImplMarcaLogica
    {
        private ImplMarcaDatos accesoDatos;

        public ImplMarcaLogica()
        {
            this.accesoDatos = new ImplMarcaDatos();
        }

        /// <summary>
        /// Listar Registros
        /// </summary>
        /// <param name="filtro">filtro de busqueda</param>
        /// <param name="numPagina">Pagina Actual</param>
        /// <param name="registrosPorPagina">Cantidad de registros a monstrar por pagina</param>
        /// <param name="totalRegistros">Total de registros en base de datos</param>
        /// <returns>Listado de registros para mostrar en la página actual que coincidan con el filtro</returns>
        public IEnumerable<MarcaDTO> ListarRegistros(String filtro, int numPagina, int registrosPorPagina, out int totalRegistros)
        {
            var listado = this.accesoDatos.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorMarcaLogica mapeador = new MapeadorMarcaLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public IEnumerable<MarcaDTO> ListarRegistrosReporte()
        {
            var listado = this.accesoDatos.ListarRegistrosReporte();
            MapeadorMarcaLogica mapeador = new MapeadorMarcaLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public MarcaDTO BuscarRegistro(int id)
        {
            var registro = this.accesoDatos.BuscarRegistro(id);
            MapeadorMarcaLogica mapeador = new MapeadorMarcaLogica();
            return mapeador.MapearTipo1Tipo2(registro);
        }

        public Boolean GuardarRegistro(MarcaDTO registro)
        {
            MapeadorMarcaLogica mapeador = new MapeadorMarcaLogica();
            MarcaDbModel reg = mapeador.MapearTipo2Tipo1(registro);
            Boolean res = this.accesoDatos.GuardarRegistro(reg);
            return res;
        }

        public Boolean EditarRegistro(MarcaDTO registro)
        {
            MapeadorMarcaLogica mapeador = new MapeadorMarcaLogica();
            MarcaDbModel reg = mapeador.MapearTipo2Tipo1(registro);
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
