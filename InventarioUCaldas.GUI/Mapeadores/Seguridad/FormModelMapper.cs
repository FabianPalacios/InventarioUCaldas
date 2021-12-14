using InventarioUCaldas.GUI.Models.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Seguridad
{
    public class FormModelMapper: MapeadorBaseGUI<FormDTO, FormModel>
    {

        /// <summary>
        /// Method to map the FormDTO object to FormModel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override FormModel MapearTipo1Tipo2(FormDTO input)
        {
            return new FormModel()
            {
                Id = input.Id,
                Name = input.Name,
                Url = input.Url,
                IsSelectedByUser = input.IsSelectedByUser
            };
        }

        public override IEnumerable<FormModel> MapearTipo1Tipo2(IEnumerable<FormDTO> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override FormDTO MapearTipo2Tipo1(FormModel input)
        {
            return new FormDTO()
            {
                Id = input.Id,
                Name = input.Name,
                Url = input.Url
            };
        }

        public override IEnumerable<FormDTO> MapearTipo2Tipo1(IEnumerable<FormModel> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}