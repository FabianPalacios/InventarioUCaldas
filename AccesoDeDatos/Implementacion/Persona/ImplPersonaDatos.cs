using AccesoDeDatos.DbModel.Persona;
using AccesoDeDatos.Mapeadores.Persona;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Implementacion.Persona
{
    public class ImplPersonaDatos
    {
        public object tb_persona;

        /// <summary>
        /// Metodo para listar Registros con un filtro
        /// </summary>
        /// <param name="filtro">Filtro a aplicar</param>
        /// <returns>Lista de registros con el filtro aplicado</returns>
        public IEnumerable<PersonaDbModel> ListarRegistros(String filtro, int paginaActual, int numRegistrosPorPagina, out int totalRegistros)
        {
            var lista = new List<PersonaDbModel>();

            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                int regDescartados = (paginaActual - 1) * numRegistrosPorPagina;
                //lista = bd.tb_persona.Where(x => x.nombre.Contains(filtro)).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();
                var listaDatos = (from m in bd.tb_persona
                                  where m.documento.Contains(filtro)
                                  select m).OrderBy(m => m.id).ToList();
                totalRegistros = listaDatos.Count();
                listaDatos = listaDatos.OrderBy(m => m.id).Skip(regDescartados).Take(numRegistrosPorPagina).ToList();
                lista = new MapeadorPersonaDatos().MapearTipo1Tipo2(listaDatos).ToList();
            }
            return lista;
        }

        /// <summary>
        /// Metodo para almacenar un registro
        /// </summary>
        /// <param name="registro">El registro a almacenar </param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción/returns>
        public bool GuardarRegistro(PersonaDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo documento
                    if (bd.tb_persona.Where(x => x.documento.ToLower().Equals(registro.Documento.ToLower())).Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorPersonaDatos mapeador = new MapeadorPersonaDatos();
                        var reg = mapeador.MapearTipo2Tipo1(registro);
                        bd.tb_persona.Add(reg);
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
        public PersonaDbModel BuscarRegistro(int id)
        {
            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                tb_persona registro = bd.tb_persona.Find(id);
                return new MapeadorPersonaDatos().MapearTipo1Tipo2(registro);
            }
        }

        /// <summary>
        /// Método de búsqueda de un registro
        /// </summary>
        /// <param name="documento">Id del registro a buscar</param>
        /// <returns>el objeto con id buscado o null cuando no exista</returns>
        public PersonaDbModel BuscarCorreoPersona(string documento)
        {
            using (InventarioUdCEntities bd = new InventarioUdCEntities())
            {
                tb_persona registro = (tb_persona)bd.tb_persona.Where(x => x.documento == documento);
                return new MapeadorPersonaDatos().MapearTipo1Tipo2(registro);
            }
        }


        /// <summary>
        /// Método para editar un registro
        /// </summary>
        /// <param name="registro">Registro editado</param>
        /// <returns>true cuando almacena, false cuando existe un registro o una excepción</returns>
        public bool EditarRegistro(PersonaDbModel registro)
        {
            try
            {
                using (InventarioUdCEntities bd = new InventarioUdCEntities())
                {
                    //Verificación de la existencia de un registro con el mismo id
                    if (bd.tb_persona.Where(x => x.id == registro.Id).Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        MapeadorPersonaDatos mapeador = new MapeadorPersonaDatos();
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
                    tb_persona registo = bd.tb_persona.Find(id);
                    if (registo == null || registo.tb_producto.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        bd.tb_persona.Remove(registo);
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
