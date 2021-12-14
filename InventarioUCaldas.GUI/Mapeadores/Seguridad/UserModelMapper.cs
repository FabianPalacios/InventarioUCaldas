using InventarioUCaldas.GUI.Models.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Seguridad
{
    public class UserModelMapper : MapeadorBaseGUI<UserDTO, UserModel>
    {
        public override UserModel MapearTipo1Tipo2(UserDTO input)
        {
            RoleModelMapper roleMapper = new RoleModelMapper();
            return new UserModel()
            {
                Id = input.Id,
                Name = input.Name,
                Lastname = input.Lastname,
                Document = input.Document,
                Cellphone = input.Cellphone,
                Email = input.Email,
                Password = input.Password,
                Roles = roleMapper.MapearTipo1Tipo2(input.Roles),
                Token = input.Token
            };
        }

        public override IEnumerable<UserModel> MapearTipo1Tipo2(IEnumerable<UserDTO> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override UserDTO MapearTipo2Tipo1(UserModel input)
        {
            return new UserDTO()
            {
                Id = input.Id,
                Name = input.Name,
                Lastname = input.Lastname,
                Document = input.Document,
                Cellphone = input.Cellphone,
                Email = input.Email,
                Password = input.Password,
                UserInSessionId = input.UserInSessionId,
                CurrentDate = input.CurrentDate
            };
        }

        public override IEnumerable<UserDTO> MapearTipo2Tipo1(IEnumerable<UserModel> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}