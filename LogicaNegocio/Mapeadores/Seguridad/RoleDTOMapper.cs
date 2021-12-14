using AccesoDeDatos.DbModel.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Mapeadores.Seguridad
{
    public class RoleDTOMapper : MapeadorBaseLogica<RoleDbModel, RoleDTO>
    {
        public override RoleDTO MapearTipo1Tipo2(RoleDbModel input)
        {

            return new RoleDTO()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Removed = input.Removed,
                IsSelectedByUser = input.IsSelectedByUser
            };
        }

        public override IEnumerable<RoleDTO> MapearTipo1Tipo2(IEnumerable<RoleDbModel> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override RoleDbModel MapearTipo2Tipo1(RoleDTO input)
        {
            return new RoleDbModel()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Removed = input.Removed
            };
        }

        public override IEnumerable<RoleDbModel> MapearTipo2Tipo1(IEnumerable<RoleDTO> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }

    }
}
