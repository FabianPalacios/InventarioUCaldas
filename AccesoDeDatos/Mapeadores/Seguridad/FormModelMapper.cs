using AccesoDeDatos.DbModel.Seguridad;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Seguridad
{
    public class FormModelMapper: MapeadorBaseDatos<SEC_FORM, FormDbModel>
    {

        /// <summary>
        /// method to map the SEC_FORM object to FormDbModel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override FormDbModel MapearTipo1Tipo2(SEC_FORM input)
        {
            return new FormDbModel()
            {
                Id = input.ID,
                Name = input.NAME,
                Url = input.URL
            };
        }

        public override IEnumerable<FormDbModel> MapearTipo1Tipo2(IEnumerable<SEC_FORM> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override SEC_FORM MapearTipo2Tipo1(FormDbModel input)
        {
            return new SEC_FORM()
            {
                ID = input.Id,
                NAME = input.Name,
                URL = input.Url
            };
        }

        public override IEnumerable<SEC_FORM> MapearTipo2Tipo1(IEnumerable<FormDbModel> input)
        {
            foreach (var item in input)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }

    }
}
