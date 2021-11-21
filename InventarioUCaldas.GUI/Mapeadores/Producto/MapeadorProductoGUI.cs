using InventarioUCaldas.GUI.Models.Producto;
using LogicaNegocio.DTO.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Mapeadores.Producto
{
    public class MapeadorProductoGUI : MapeadorBaseGUI<ProductoDTO, ModeloProductoGUI>
    {
        public override ModeloProductoGUI MapearTipo1Tipo2(ProductoDTO entrada)
        {
            return new ModeloProductoGUI()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                FechaRegistro = entrada.FechaRegistro,
                IdMarca = entrada.IdMarca,
                IdCategoria = entrada.IdCategoria,
                IdTipoProducto = entrada.IdTipoProducto,
                Serial = entrada.Serial,
                IdEspacio = entrada.IdEspacio,
                IdPersona = entrada.IdPersona,
                Estado = entrada.Estado,

                NombreMarca = entrada.NombreMarca,
                NombreCategoria = entrada.NombreCategoria,
                NombreProducto = entrada.NombreProducto,
                NombreEspacio = entrada.NombreEspacio,
                DocumentoPersona = entrada.DocumentoPersona
            };
        }

        public override IEnumerable<ModeloProductoGUI> MapearTipo1Tipo2(IEnumerable<ProductoDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override ProductoDTO MapearTipo2Tipo1(ModeloProductoGUI entrada)
        {
            return new ProductoDTO()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                FechaRegistro = entrada.FechaRegistro,
                IdMarca = entrada.IdMarca,
                IdCategoria = entrada.IdCategoria,
                IdTipoProducto = entrada.IdTipoProducto,
                Serial = entrada.Serial,
                IdEspacio = entrada.IdEspacio,
                IdPersona = entrada.IdPersona,
                Estado = entrada.Estado
            };
        }

        public override IEnumerable<ProductoDTO> MapearTipo2Tipo1(IEnumerable<ModeloProductoGUI> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}