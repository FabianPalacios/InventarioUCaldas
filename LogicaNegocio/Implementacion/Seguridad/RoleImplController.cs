using AccesoDeDatos.DbModel.Seguridad;
using AccesoDeDatos.Implementacion.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using LogicaNegocio.Mapeadores.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Implementacion.Seguridad
{
    public class RoleImplController
    {
        
        private RoleImplModel model;
        public RoleImplController()
        {
            model = new RoleImplModel();
        }

        /// <summary>
        /// Creación de un registro
        /// </summary>
        /// <param name="dto">Información dto</param>
        /// <returns>1: Ok, 2: Excepción, 3: Ya existe</returns>
        public int RecordCreation(RoleDTO dto)
        {
            RoleDTOMapper mapper = new RoleDTOMapper();
            RoleDbModel dbModel = mapper.MapearTipo2Tipo1(dto);
            return model.RecordCreation(dbModel);
        }

        /// <summary>
        /// Actualización de un registro
        /// </summary>
        /// <param name="dto">Información dto</param>
        /// <returns>1: Ok, 2: Excepción, 3: Ya existe</returns>

        public int RecordUpdate(RoleDTO dto)
        {
            RoleDTOMapper mapper = new RoleDTOMapper();
            RoleDbModel dbModel = mapper.MapearTipo2Tipo1(dto);
            return model.RecordUpdate(dbModel);
        }

        /// <summary>
        /// Eliminación de un registro
        /// </summary>
        /// <param name="dto">Información dto</param>
        /// <returns>1: Ok, 2: Excepción, 3: Ya existe</returns>

        public int RecordRemove(RoleDTO dto)
        {
            RoleDTOMapper mapper = new RoleDTOMapper();
            RoleDbModel dbModel = mapper.MapearTipo2Tipo1(dto);
            return model.RecordRemove(dbModel);
        }

        /// <summary>
        /// Se listan basandose en un filtro los registros de los roles
        /// </summary>
        /// <param name="filter">Representa filtro</param>
        /// <returns>Lista con los roles, teniendo su id y su nombre respectivo.</returns>

        public IEnumerable<RoleDTO> RecordList(string filter)
        {
            var list = model.RecordList(filter);
            RoleDTOMapper mapper = new RoleDTOMapper();
            return mapper.MapearTipo1Tipo2(list);            
        }

        /// <summary>
        /// Busqueda de un rol
        /// </summary>
        /// <param name="id">Id del rol</param>
        /// <returns>El registro</returns>
        public RoleDTO RecordSearch(int id)
        {
            var record = model.RecordSearch(id);
            if (record == null)
            {
                return null;
            }
            RoleDTOMapper mapper = new RoleDTOMapper();
            return mapper.MapearTipo1Tipo2(record);
        }

        public IEnumerable<FormDTO> RecordFormList()
        {
            var list = model.RecordFormList();
            FormDTOMapper mapper = new FormDTOMapper();
            return mapper.MapearTipo1Tipo2(list);
        }

        public IEnumerable<FormDTO> RecordFormListByRole(int roleId)
        {
            var list = model.RecordFormListByRole(roleId);
            FormDTOMapper mapper = new FormDTOMapper();
            return mapper.MapearTipo1Tipo2(list);
        }

        public bool AssignForms(List<int> formsList, int roleId)
        {
            return model.AssignForms(formsList, roleId);
        }

    }
}
