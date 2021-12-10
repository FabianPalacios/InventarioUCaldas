using InventarioUCaldas.GUI.Models.Parametros;
using InventarioUCaldas.GUI.Models.Producto;
using LogicaNegocio.DTO.Parametros;
using LogicaNegocio.DTO.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Producto
{
    public class MapeadorFotoProductoGUI : MapeadorBaseGUI<FotoProductoDTO, ModeloFotoProductoGUI>
    {
        public override ModeloFotoProductoGUI MapearTipo1Tipo2(FotoProductoDTO entrada)
        {
            return new ModeloFotoProductoGUI()
            {
                Id = entrada.Id,
                IdProducto = entrada.IdProducto,
                NombreFoto = entrada.NombreFoto
            };
        }

        public override IEnumerable<ModeloFotoProductoGUI> MapearTipo1Tipo2(IEnumerable<FotoProductoDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override FotoProductoDTO MapearTipo2Tipo1(ModeloFotoProductoGUI entrada)
        {
            return new FotoProductoDTO()
            {
                Id = entrada.Id,
                IdProducto = entrada.IdProducto,
                NombreFoto = entrada.NombreFoto
            };
        }

        public override IEnumerable<FotoProductoDTO> MapearTipo2Tipo1(IEnumerable<ModeloFotoProductoGUI> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}