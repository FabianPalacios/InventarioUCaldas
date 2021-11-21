using InventarioUCaldas.GUI.Models.Ubicacion;
using LogicaNegocio.DTO.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Ubicacion
{
    public class MapeadorSedeGUI : MapeadorBaseGUI<SedeDTO, ModeloSedeGUI>
    {
        public override ModeloSedeGUI MapearTipo1Tipo2(SedeDTO entrada)
        {
            return new ModeloSedeGUI()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                Direccion = entrada.Direccion
            };
        }

        public override IEnumerable<ModeloSedeGUI> MapearTipo1Tipo2(IEnumerable<SedeDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override SedeDTO MapearTipo2Tipo1(ModeloSedeGUI entrada)
        {
            return new SedeDTO()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                Direccion = entrada.Direccion
            };
        }

        public override IEnumerable<SedeDTO> MapearTipo2Tipo1(IEnumerable<ModeloSedeGUI> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}