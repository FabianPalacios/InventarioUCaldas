using InventarioUCaldas.GUI.Models.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Seguridad
{
    public class RoleModelMapper : MapeadorBaseGUI<RoleDTO, RoleModel>
    {

        /// <summary>
        /// Method to map the RoleDTO object to RoleModel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override RoleModel MapearTipo1Tipo2(RoleDTO input)
        {
            return new RoleModel()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Removed = input.Removed,
                IsSelectedByUser = input.IsSelectedByUser
            };
        }

        public override IEnumerable<RoleModel> MapearTipo1Tipo2(IEnumerable<RoleDTO> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override RoleDTO MapearTipo2Tipo1(RoleModel input)
        {
            return new RoleDTO()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Removed = input.Removed
            };
        }

        public override IEnumerable<RoleDTO> MapearTipo2Tipo1(IEnumerable<RoleModel> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}