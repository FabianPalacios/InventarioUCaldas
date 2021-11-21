using InventarioUCaldas.GUI.Models.Persona;
using LogicaNegocio.DTO.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Persona
{
    public class MapeadorPersonaGUI : MapeadorBaseGUI<PersonaDTO, ModeloPersonaGUI>
    {
        public override ModeloPersonaGUI MapearTipo1Tipo2(PersonaDTO entrada)
        {
            return new ModeloPersonaGUI()
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

        public override IEnumerable<ModeloPersonaGUI> MapearTipo1Tipo2(IEnumerable<PersonaDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override PersonaDTO MapearTipo2Tipo1(ModeloPersonaGUI entrada)
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

        public override IEnumerable<PersonaDTO> MapearTipo2Tipo1(IEnumerable<ModeloPersonaGUI> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}