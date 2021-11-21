using InventarioUCaldas.GUI.Models.Ubicacion;
using LogicaNegocio.DTO.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Ubicacion
{
    public class MapeadorEdificioGUI : MapeadorBaseGUI<EdificioDTO, ModeloEdificioGUI>
    {
        public override ModeloEdificioGUI MapearTipo1Tipo2(EdificioDTO entrada)
        {
            return new ModeloEdificioGUI()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                Bloque = entrada.Bloque,
                IdSede = entrada.IdSede,
                NombreSede = entrada.NombreSede
            };
        }

        public override IEnumerable<ModeloEdificioGUI> MapearTipo1Tipo2(IEnumerable<EdificioDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override EdificioDTO MapearTipo2Tipo1(ModeloEdificioGUI entrada)
        {
            return new EdificioDTO()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                Bloque = entrada.Bloque,
                IdSede = entrada.IdSede
            };
        }

        public override IEnumerable<EdificioDTO> MapearTipo2Tipo1(IEnumerable<ModeloEdificioGUI> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}