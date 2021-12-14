using AccesoDeDatos.DbModel.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Mapeadores.Seguridad
{
    class FormDTOMapper : MapeadorBaseLogica<FormDbModel, FormDTO>
    {

        /// <summary>
        /// Method to map teh FOrmDbModel object to FormDTO
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override FormDTO MapearTipo1Tipo2(FormDbModel input)
        {
            return new FormDTO()
            {
                Id = input.Id,
                Name = input.Name,
                Url = input.Url,
                IsSelectedByUser = input.IsSelectedByUser
            };
        }

        public override IEnumerable<FormDTO> MapearTipo1Tipo2(IEnumerable<FormDbModel> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override FormDbModel MapearTipo2Tipo1(FormDTO input)
        {
            return new FormDbModel()
            {
                Id = input.Id,
                Name = input.Name,
                Url = input.Url
            };
        }

        public override IEnumerable<FormDbModel> MapearTipo2Tipo1(IEnumerable<FormDTO> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }

    }
}
