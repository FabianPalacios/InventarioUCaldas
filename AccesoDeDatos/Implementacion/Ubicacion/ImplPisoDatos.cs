using AccesoDeDatos.DbModel.Ubicacion;
using AccesoDeDatos.Mapeadores.Ubicacion;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Implementacion.Ubicacion
{
    public class ImplPisoDatos
    {
        public object tb_piso;

        /// <summary>
        /// Metodo para listar Registros con un filtro
        /// </summary>
        /// <param name="filtro">Filtro a aplicar</param>
        /// <returns>Lista de registros con el filtro aplicado</returns>
        public IEnumerable<PisoDbModel> ListarRegistros(String filtro, int paginaActual, int numRegistrosPorPagina, out int totalRegistros)
        {
            var lista = new List<PisoDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                int regDescartados = (paginaActual - 1) * numRegistrosPorPagina;
                //lista = bd.tb_piso.Where(x => x.nombre.Contains(filtro)).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();
                var listaDatos = (from p in bd.tb_piso
                                  where p.nombre.Contains(filtro)
                                  select p).OrderBy(m => m.id).ToList();
                totalRegistros = lista.Count();
                listaDatos = listaDatos.OrderBy(m => m.id).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();
                lista = new MapeadorPisoDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        public IEnumerable<EdificioDbModel> ListarRegistrosEdificio()
        {
            var lista = new List<EdificioDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                var listaDatos = (from e in bd.tb_edificio
                                  select e).ToList();
                lista = new MapeadorEdificioDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        /// <summary>
        /// Metodo para almacenar un registro
        /// </summary>
        /// <param name="registro">El registro a almacenar </param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción/returns>
        public bool GuardarRegistro(PisoDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo nombre
                    if (bd.tb_piso.Where(x => x.nombre.ToLower().Equals(registro.Nombre.ToLower())).Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorPisoDatos mapeador = new MapeadorPisoDatos();
                        var reg = mapeador.MapearTipo2Tipo1(registro);
                        bd.tb_piso.Add(reg);
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
        public PisoDbModel BuscarRegistro(int id)
        {
            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                tb_piso registro = bd.tb_piso.Find(id);
                return new MapeadorPisoDatos().MapearTipo1Tipo2(registro);
            }
        }


        /// <summary>
        /// Método para editar un registro
        /// </summary>
        /// <param name="registro">Registro editado</param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción</returns>
        public bool EditarRegistro(PisoDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo id
                    if (bd.tb_piso.Where(x => x.id == registro.Id).Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorPisoDatos mapeador = new MapeadorPisoDatos();
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
                    tb_piso registo = bd.tb_piso.Find(id);
                    if (registo == null || registo.tb_espacio.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        bd.tb_piso.Remove(registo);
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
