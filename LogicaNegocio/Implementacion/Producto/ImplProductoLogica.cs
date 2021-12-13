using AccesoDeDatos.DbModel.Producto;
using AccesoDeDatos.Implementacion.Producto;
using LogicaNegocio.DTO.Parametros;
using LogicaNegocio.DTO.Persona;
using LogicaNegocio.DTO.Producto;
using LogicaNegocio.DTO.Ubicacion;
using LogicaNegocio.Mapeadores.Parametros;
using LogicaNegocio.Mapeadores.Persona;
using LogicaNegocio.Mapeadores.Pr;
using LogicaNegocio.Mapeadores.Producto;
using LogicaNegocio.Mapeadores.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Implementacion.Producto
{
    public class ImplProductoLogica
    {
        private ImplProductoDatos accesoDatos;

        public ImplProductoLogica()
        {
            this.accesoDatos = new ImplProductoDatos();
        }

        /// <summary>
        /// Listar Registros
        /// </summary>
        /// <param name="filtro">filtro de busqueda</param>
        /// <param name="numPagina">Pagina Actual</param>
        /// <param name="registrosPorPagina">Cantidad de registros a monstrar por pagina</param>
        /// <param name="totalRegistros">Total de registros en base de datos</param>
        /// <returns>Listado de registros para mostrar en la página actual que coincidan con el filtro</returns>
        public IEnumerable<ProductoDTO> ListarRegistros(String filtro, int numPagina, int registrosPorPagina, out int totalRegistros)
        {
            var listado = this.accesoDatos.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorProductoLogica mapeador = new MapeadorProductoLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public IEnumerable<ProductoDTO> ListarProductosPersona(int id)
        {
            var listado = this.accesoDatos.ListarProductosPersona(id);
            MapeadorProductoLogica mapeador = new MapeadorProductoLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public IEnumerable<MarcaDTO> ListarRegistrosMarca()
        {
            var listado = this.accesoDatos.ListarRegistrosMarca();
            MapeadorMarcaLogica mapeador = new MapeadorMarcaLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public IEnumerable<CategoriaDTO> ListarRegistrosCategoria()
        {
            var listado = this.accesoDatos.ListarRegistrosCategoria();
            MapeadorCategoriaLogica mapeador = new MapeadorCategoriaLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public IEnumerable<TipoProductoDTO> ListarRegistrosTipoProducto()
        {
            var listado = this.accesoDatos.ListarRegistrosTipoProducto();
            MapeadorTipoProductoLogica mapeador = new MapeadorTipoProductoLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public IEnumerable<EspacioDTO> ListarRegistrosEspacio()
        {
            var listado = this.accesoDatos.ListarRegistrosEspacio();
            MapeadorEspacioLogica mapeador = new MapeadorEspacioLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public IEnumerable<PersonaDTO> ListarRegistrosPersona()
        {
            var listado = this.accesoDatos.ListarRegistrosPersona();
            MapeadorPersonaLogica mapeador = new MapeadorPersonaLogica();
            return mapeador.MapearTipo1Tipo2(listado);
        }

        public ProductoDTO BuscarRegistro(int id)
        {
            var registro = this.accesoDatos.BuscarRegistro(id);
            MapeadorProductoLogica mapeador = new MapeadorProductoLogica();
            return mapeador.MapearTipo1Tipo2(registro);
        }

        public Boolean GuardarRegistro(ProductoDTO registro)
        {
            MapeadorProductoLogica mapeador = new MapeadorProductoLogica();
            ProductoDbModel reg = mapeador.MapearTipo2Tipo1(registro);
            Boolean res = this.accesoDatos.GuardarRegistro(reg);
            return res;
        }

        public Boolean EditarRegistro(ProductoDTO registro)
        {
            MapeadorProductoLogica mapeador = new MapeadorProductoLogica();
            ProductoDbModel reg = mapeador.MapearTipo2Tipo1(registro);
            Boolean res = this.accesoDatos.EditarRegistro(reg);
            return res;
        }

        public Boolean EliminarRegistro(int id)
        {
            Boolean res = this.accesoDatos.EliminarRegistro(id);
            return res;
        }

        public Boolean EliminarRegistroFoto(int id)
        {
            Boolean res = this.accesoDatos.EliminarRegistroFoto(id);
            return res;
        }

        public Boolean GuardarNombreFoto(FotoProductoDTO dto)

        {
            MapeadorFotoProductoLogica mapeador = new MapeadorFotoProductoLogica();
            FotoProductoDbModel dbModel = mapeador.MapearTipo2Tipo1(dto);
            bool res = this.accesoDatos.GuardarFotoProducto(dbModel);
            return res;
        } 

        public IEnumerable<FotoProductoDTO> listarFotoProductoPorId(int idVehiculo)
        {
            IEnumerable<FotoProductoDbModel> listaDbModel = this.accesoDatos.ListarFotosProductoPorId(idVehiculo);
            MapeadorFotoProductoLogica mapeador = new MapeadorFotoProductoLogica();
            IEnumerable<FotoProductoDTO> lista = mapeador.MapearTipo1Tipo2(listaDbModel);
            return lista;
        }
    }
}
