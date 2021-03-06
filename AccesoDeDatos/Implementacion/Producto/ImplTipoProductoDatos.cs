using AccesoDeDatos.DbModel.Producto;
using AccesoDeDatos.Mapeadores.Producto;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AccesoDeDatos.Implementacion.Producto
{
    public class ImplTipoProductoDatos
    {
        public object tb_tipoProducto;

        /// <summary>
        /// Metodo para listar Registros con un filtro
        /// </summary>
        /// <param name="filtro">Filtro a aplicar</param>
        /// <returns>Lista de registros con el filtro aplicado</returns>
        public IEnumerable<TipoProductoDbModel> ListarRegistros(String filtro, int paginaActual, int numRegistrosPorPagina, out int totalRegistros)
        {
            var lista = new List<TipoProductoDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                int regDescartados = (paginaActual - 1) * numRegistrosPorPagina;
                //lista = bd.tb_tipoProducto.Where(x => x.nombre.Contains(filtro)).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();
                var listaDatos = (from t in bd.tb_tipoProducto
                                  where t.nombre.Contains(filtro)
                                  select t).OrderBy(m => m.id).ToList();
                totalRegistros = listaDatos.Count();
                listaDatos = listaDatos.OrderBy(m => m.id).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();
                lista = new MapeadorTipoProductoDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        /// <summary>
        /// Metodo para almacenar un registro
        /// </summary>
        /// <param name="registro">El registro a almacenar </param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción/returns>
        public bool GuardarRegistro(TipoProductoDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo nombre
                    if (bd.tb_tipoProducto.Where(x => x.nombre.ToLower().Equals(registro.Nombre.ToLower())).Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorTipoProductoDatos mapeador = new MapeadorTipoProductoDatos();
                        var reg = mapeador.MapearTipo2Tipo1(registro);
                        bd.tb_tipoProducto.Add(reg);
                        bd.SaveChanges();
                        return true;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Método de búsqueda de un registro
        /// </summary>
        /// <param name="id">Id del registro a buscar</param>
        /// <returns>el objeto con id buscado o null cuando no exista</returns>
        public TipoProductoDbModel BuscarRegistro(int id)
        {
            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                tb_tipoProducto registro = bd.tb_tipoProducto.Find(id);
                return new MapeadorTipoProductoDatos().MapearTipo1Tipo2(registro);
            }
        }


        /// <summary>
        /// Método para editar un registro
        /// </summary>
        /// <param name="registro">Registro editado</param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción</returns>
        public bool EditarRegistro(TipoProductoDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo id
                    if (bd.tb_tipoProducto.Where(x => x.id == registro.Id).Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorTipoProductoDatos mapeador = new MapeadorTipoProductoDatos();
                        var reg = mapeador.MapearTipo2Tipo1(registro);
                        bd.Entry(reg).State = EntityState.Modified;
                        bd.SaveChanges();
                        return true;
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Metodo de eliminar un registro por id
        /// </summary>
        /// <param name="id">Id del registro a elimniar</param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción</returns>
        public bool EliminarRegistro(int id)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {

                    //Verificación de la existencia de un registro con el mismo id
                    tb_tipoProducto registo = bd.tb_tipoProducto.Find(id);
                    if (registo == null || registo.tb_producto.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        bd.tb_tipoProducto.Remove(registo);
                        bd.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
