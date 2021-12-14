using InventarioUCaldas.GUI.Mapeadores.Seguridad;
using InventarioUCaldas.GUI.Models.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using LogicaNegocio.Implementacion.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Helpers
{
    public static class Menu
    {
        public static IEnumerable<FormModel> GetMenuForms(int userId)
        {
            UserImplController controller = new UserImplController();
            IEnumerable<FormDTO> dtoList = controller.GetRoleFormsByUser(userId);
            FormModelMapper mapper = new FormModelMapper();
            IEnumerable<FormModel> list = mapper.MapearTipo1Tipo2(dtoList);
            return list;
        }
    }
}