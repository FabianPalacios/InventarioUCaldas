using InventarioUCaldas.GUI.Models.Ubicacion;
using LogicaNegocio.DTO.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Ubicacion
{
    public class MapeadorEspacioGUI : MapeadorBaseGUI<EspacioDTO, ModeloEspacioGUI>
    {
        public override ModeloEspacioGUI MapearTipo1Tipo2(EspacioDTO entrada)
        {
            return new ModeloEspacioGUI()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                IdPiso = entrada.IdPiso,
                NombrePiso = entrada.NombrePiso
            };
        }

        public override IEnumerable<ModeloEspacioGUI> MapearTipo1Tipo2(IEnumerable<EspacioDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override EspacioDTO MapearTipo2Tipo1(ModeloEspacioGUI entrada)
        {
            return new EspacioDTO()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                IdPiso  = entrada.IdPiso
            };
        }

        public override IEnumerable<EspacioDTO> MapearTipo2Tipo1(IEnumerable<ModeloEspacioGUI> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}