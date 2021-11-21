using AccesoDeDatos.DbModel.Persona;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Persona
{
    public class MapeadorPersonaDatos : MapeadorBaseDatos<tb_persona, PersonaDbModel>
    {
        public override PersonaDbModel MapearTipo1Tipo2(tb_persona entrada)
        {
            return new PersonaDbModel()
            {
                Id = entrada.id,
                Primer_Nombre = entrada.primer_nombre,
                Otros_Nombres = entrada.otros_nombres,
                Primer_Apellido = entrada.primer_apellido,
                Segundo_Apellido = entrada.segundo_apellido,
                Documento = entrada.documento,
                Celular = entrada.celular,
                Correo = entrada.correo
            };
        }

        public override IEnumerable<PersonaDbModel> MapearTipo1Tipo2(IEnumerable<tb_persona> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_persona MapearTipo2Tipo1(PersonaDbModel entrada)
        {
            return new tb_persona()
            {
                id = entrada.Id,
                primer_nombre = entrada.Primer_Nombre,
                otros_nombres = entrada.Otros_Nombres,
                primer_apellido = entrada.Primer_Apellido,
                segundo_apellido = entrada.Segundo_Apellido,
                documento = entrada.Documento,
                celular = entrada.Celular,
                correo = entrada.Correo
            };
        }

        public override IEnumerable<tb_persona> MapearTipo2Tipo1(IEnumerable<PersonaDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
