using AccesoDeDatos.DbModel.Persona;
using LogicaNegocio.DTO.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Mapeadores.Persona
{
    public class MapeadorPersonaLogica : MapeadorBaseLogica<PersonaDbModel, PersonaDTO>
    {
        public override PersonaDTO MapearTipo1Tipo2(PersonaDbModel entrada)
        {
            return new PersonaDTO()
            {
                Id = entrada.Id,
                Primer_Nombre = entrada.Primer_Nombre,
                Otros_Nombres = entrada.Otros_Nombres,
                Primer_Apellido = entrada.Primer_Apellido,
                Segundo_Apellido = entrada.Segundo_Apellido,
                Documento = entrada.Documento,
                Celular = entrada.Celular,
                Correo = entrada.Correo
            };
        }

        public override IEnumerable<PersonaDTO> MapearTipo1Tipo2(IEnumerable<PersonaDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override PersonaDbModel MapearTipo2Tipo1(PersonaDTO entrada)
        {
            return new PersonaDbModel()
            {
                Id = entrada.Id,
                Primer_Nombre = entrada.Primer_Nombre,
                Otros_Nombres = entrada.Otros_Nombres,
                Primer_Apellido = entrada.Primer_Apellido,
                Segundo_Apellido = entrada.Segundo_Apellido,
                Documento = entrada.Documento,
                Celular = entrada.Celular,
                Correo = entrada.Correo
            };
        }

        public override IEnumerable<PersonaDbModel> MapearTipo2Tipo1(IEnumerable<PersonaDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
