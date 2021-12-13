using AccesoDeDatos.DbModel.Seguridad;
using AccesoDeDatos.Mapeadores.Seguridad;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Implementacion.Seguridad
{
    public class RoleImplModel
    {
        /// <summary>
        /// Se agrega un nuevo registro a los roles
        /// </summary>
        /// <param name="dbModel">Representa un objeto con informacion del rol</param>
        /// <returns>entero con la respuesta 1.OK 2.KO 3.Ya existe</returns>
        public int RecordCreation(RoleDbModel dbModel)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                try
                {
                    //verifica si existe un rol con el nombre que se quiere crear el nuevo
                    if (db.SEC_ROLE.Where(x => x.NAME.ToUpper().Equals(dbModel.Name.ToUpper())).Count() > 0)
                    {
                        return 3;
                    }

                    RoleModelMapper mapper = new RoleModelMapper();
                    SEC_ROLE record = mapper.MapearTipo2Tipo1(dbModel);
                    db.SEC_ROLE.Add(record);
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 2;
                }
            }
        }

        /// <summary>
        /// Se actualiza un registro de un rol
        /// </summary>
        /// <param name="dbModel">Representa un objeto con informacion del rol</param>
        /// <returns>entero con la respuesta 1.OK 2.KO 3.Ya existe</returns>

        public int RecordUpdate(RoleDbModel dbModel)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                try
                {
                    var record = db.SEC_ROLE.Where(x => x.ID == dbModel.Id).FirstOrDefault();

                    //verifica si existe un registro
                    if (record == null)
                    {
                        return 3;
                    }

                    record.NAME = dbModel.Name;
                    record.DESCRIPTION = dbModel.Description;
                    record.REMOVED = dbModel.Removed;

                    db.Entry(record).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 2;
                }
            }
        }


        /// <summary>
        /// Se elimina un registro de un rol en la interfaz gráfica.
        /// </summary>
        /// <param name="dbModel">Representa un objeto con informacion del rol</param>
        /// <returns>entero con la respuesta 1.OK 2.KO 3.Ya existe</returns>

        public int RecordRemove(RoleDbModel dbModel)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                try
                {
                    var record = db.SEC_ROLE.Where(x => x.ID == dbModel.Id).FirstOrDefault();

                    //verifica si existe un registro
                    if (record == null)
                    {
                        return 3;
                    }

                    //Este se utilizaría para eliminar totalmente de la DB.
                    //db.SEC_ROLE.Remove(record); 

                    //Aquí se elimina de la interfaz gráfica, pero queda el historial.
                    record.REMOVED = true;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 2;
                }
            }
        }

        /// <summary>
        /// Se listan basandose en un filtro los registros de los roles
        /// </summary>
        /// <param name="filter">Representa un filtro</param>
        /// <returns>Lista con los roles, teniendo su id y su nombre respectivo.</returns>

        public IEnumerable<RoleDbModel> RecordList(String filter)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                //Método Lambda
                //var listaLambda = db.SEC_ROLE.Where(x => !x.REMOVED && x.NAME.ToUpper().Contains(filter.ToUpper())).ToList();

                //Método por Linq, similar a consultas SQL
                var listaLinq = from role in db.SEC_ROLE
                                where !role.REMOVED && role.NAME.ToUpper().Contains(filter)
                                select role;

                RoleModelMapper mapper = new RoleModelMapper();

                //El mapeo se puede realizar con cualquiera de los dos métodos.
                var listaFinal = mapper.MapearTipo1Tipo2(listaLinq);

                return listaFinal.ToList();
            }
        }

        /// <summary>
        /// Búsqueda de roles
        /// </summary>
        /// <param name="id">Representa el id del rol</param>
        /// <returns>El registro.</returns>

        public RoleDbModel RecordSearch(int id)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                //Método Lambda
                var record = db.SEC_ROLE.Where(x => !x.REMOVED && x.ID == id).FirstOrDefault();
                if (record != null)
                {
                    RoleModelMapper mapper = new RoleModelMapper();
                    return mapper.MapearTipo1Tipo2(record);
                }
                return null;
            }
        }

        /// <summary>
        /// Se listan los roles que tiene un usuario
        /// </summary>
        /// <param name="userId">Representa el ud del usuario</param>
        /// <returns>Lista con los roles de un usuario, teniendo en cuenta su id.</returns>

        public IEnumerable<RoleDbModel> RecordListByUser(int userId)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {

                var roleListDB = from role in db.SEC_ROLE
                                 where !role.REMOVED
                                 select role;

                var roleListDbModel = new List<RoleDbModel>();

                foreach (var role in roleListDB)
                {
                    roleListDbModel.Add(new RoleDbModel()
                    {
                        Id = role.ID,
                        Name = role.NAME,
                        Description = role.DESCRIPTION,
                        Removed = role.REMOVED,
                        IsSelectedByUser = db.SEC_USER_ROLE.Where(x => x.ROLEID == role.ID && x.USERID == userId).Count() > 0
                    });

                }
                return roleListDbModel.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="forms"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool AssignForms(List<int> forms, int roleId)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                try
                {
                    IList<SEC_FORMS_ROLE> formsRoleList = db.SEC_FORMS_ROLE.Where(x => x.ROLE_ID == roleId).ToList();
                    foreach (var formsRole in formsRoleList)
                    {
                        db.SEC_FORMS_ROLE.Remove(formsRole);
                    }

                    foreach (int formsId in forms)
                    {
                        db.SEC_FORMS_ROLE.Add(new SEC_FORMS_ROLE()
                        {
                            ROLE_ID = roleId,
                            FORM_ID = formsId
                        });
                    }
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public IEnumerable<FormDbModel> RecordFormList()
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                var recordList = from f in db.SEC_FORM
                                 select f;

                FormModelMapper mapper = new FormModelMapper();
                var listaFinal = mapper.MapearTipo1Tipo2(recordList).ToList();

                return listaFinal.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<FormDbModel> RecordFormListByRole(int roleId)
        {
            using (SeguridadUdCEntities db = new SeguridadUdCEntities())
            {
                var recordList = from f in db.SEC_FORM
                                 select f;
                IList<FormDbModel> listaFinal = new List<FormDbModel>();
                foreach (var f in recordList)
                {
                    listaFinal.Add(new FormDbModel()
                    {
                        Id = f.ID,
                        Name = f.NAME,
                        Url = f.URL,
                        IsSelectedByUser = f.SEC_FORMS_ROLE.Where(x => x.ROLE_ID == roleId).Count() > 0
                    });
                }

                return listaFinal;
            }
        }

    }
}
