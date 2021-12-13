using AccesoDeDatos.DbModel.Parametros;
using AccesoDeDatos.DbModel.Persona;
using AccesoDeDatos.DbModel.Producto;
using AccesoDeDatos.DbModel.Ubicacion;
using AccesoDeDatos.Mapeadores.Parametros;
using AccesoDeDatos.Mapeadores.Persona;
using AccesoDeDatos.Mapeadores.Producto;
using AccesoDeDatos.Mapeadores.Ubicacion;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Implementacion.Producto
{
    public class ImplProductoDatos
    {
        public object tb_producto;

        /// <summary>
        /// Metodo para listar Registros con un filtro
        /// </summary>
        /// <param name="filtro">Filtro a aplicar</param>
        /// <returns>Lista de registros con el filtro aplicado</returns>
        public IEnumerable<ProductoDbModel> ListarRegistros(String filtro, int paginaActual, int numRegistrosPorPagina, out int totalRegistros)
        {
            var lista = new List<ProductoDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                int regDescartados = (paginaActual - 1) * numRegistrosPorPagina;
                //lista = bd.tb_producto.Where(x => x.nombre.Contains(filtro)).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();


                var listaDatos = (from m in bd.tb_producto
                                  join e in bd.tb_espacio on m.id_espacio equals e.id
                                  join p in bd.tb_persona on m.id_persona equals p.id
                                  join z in bd.tb_marca on m.id_marca equals z.id
                                  join t in bd.tb_tipoProducto on m.id_tipo_producto equals t.id
                                  join c in bd.tb_categoria on m.id_categoria equals c.id
                                  where m.nombre.Contains(filtro) || m.serial.Contains(filtro) || e.nombre.Contains(filtro) || p.documento.Contains(filtro) 
                                  || z.nombre.Contains(filtro) || c.nombre.Contains(filtro) || t.nombre.Contains(filtro)
                                  select m).OrderBy(m => m.id).ToList();
                totalRegistros = listaDatos.Count();
                listaDatos = listaDatos.OrderBy(m => m.id).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();
                lista = new MapeadorProductoDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        public IEnumerable<ProductoDbModel> ListarProductosPersona(int idPersona)
        {
            var lista = new List<ProductoDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                var listaDatos = (from m in bd.tb_producto
                                  where m.id_persona.Equals(idPersona)
                                  select m).OrderBy(m => m.id).ToList();
                lista = new MapeadorProductoDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        public IEnumerable<MarcaDbModel> ListarRegistrosMarca()
        {
            var lista = new List<MarcaDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                var listaDatos = (from e in bd.tb_marca
                                  select e).ToList();
                lista = new MapeadorMarcaDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        public IEnumerable<CategoriaDbModel> ListarRegistrosCategoria()
        {
            var lista = new List<CategoriaDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                var listaDatos = (from e in bd.tb_categoria
                                  select e).ToList();
                lista = new MapeadorCategoriaDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        public IEnumerable<TipoProductoDbModel> ListarRegistrosTipoProducto()
        {
            var lista = new List<TipoProductoDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                var listaDatos = (from e in bd.tb_tipoProducto
                                  select e).ToList();
                lista = new MapeadorTipoProductoDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        public IEnumerable<EspacioDbModel> ListarRegistrosEspacio()
        {
            var lista = new List<EspacioDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                var listaDatos = (from e in bd.tb_espacio
                                  select e).ToList();
                lista = new MapeadorEspacioDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        public IEnumerable<PersonaDbModel> ListarRegistrosPersona()
        {
            var lista = new List<PersonaDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                var listaDatos = (from e in bd.tb_persona
                                  select e).ToList();
                lista = new MapeadorPersonaDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        /// <summary>
        /// Metodo para almacenar un registro
        /// </summary>
        /// <param name="registro">El registro a almacenar </param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción/returns>
        public bool GuardarRegistro(ProductoDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo nombre
                    if (bd.tb_producto.Where(x => x.nombre.ToLower().Equals(registro.Nombre.ToLower())).Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorProductoDatos mapeador = new MapeadorProductoDatos();
                        var reg = mapeador.MapearTipo2Tipo1(registro);
                        bd.tb_producto.Add(reg);
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
        public ProductoDbModel BuscarRegistro(int id)
        {
            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                tb_producto registro = bd.tb_producto.Find(id);
                return new MapeadorProductoDatos().MapearTipo1Tipo2(registro);
            }
        }


        /// <summary>
        /// Método para editar un registro
        /// </summary>
        /// <param name="registro">Registro editado</param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción</returns>
        public bool EditarRegistro(ProductoDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo id
                    if (bd.tb_producto.Where(x => x.id == registro.Id).Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorProductoDatos mapeador = new MapeadorProductoDatos();
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
                    tb_producto registo = bd.tb_producto.Find(id);
                    if (registo == null )
                    {
                        return false;
                    }
                    else
                    {
                        bd.tb_producto.Remove(registo);
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

        public bool EliminarRegistroFoto(int id)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {

                    //Verificación de la existencia de un registro con el mismo id
                    tb_foto registo = bd.tb_foto.Find(id);
                    if (registo == null)
                    {
                        return false;
                    }
                    else
                    {
                        bd.tb_foto.Remove(registo);
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

        public bool GuardarFotoProducto(FotoProductoDbModel dbModel)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    if (bd.tb_producto.Where(x => x.id == dbModel.IdProducto).Count() > 0)
                    {
                        MapeadorFotoProductoDatos mapeador = new MapeadorFotoProductoDatos();
                        tb_foto foto = mapeador.MapearTipo2Tipo1(dbModel);
                        bd.tb_foto.Add(foto);
                        bd.SaveChanges();
                        return true;
                    }
                    return false;
                }

            }
            catch(Exception e)
            {
                throw e;
            }


        }

        public IEnumerable<FotoProductoDbModel> ListarFotosProductoPorId(int id)
        {
            using (InventarioUdCEntities db = new InventarioUdCEntities())
            {
               // var lista = db.tb_foto.Where(x => x.id_producto == id).ToList();
                var lista = (from f in db.tb_foto
                             where f.id_producto == id
                             select f).ToList();

                MapeadorFotoProductoDatos mapeador = new MapeadorFotoProductoDatos();
                IEnumerable<FotoProductoDbModel> listaDbModel = mapeador.MapearTipo1Tipo2(lista);
                return listaDbModel;
            }
        }
    }
}
