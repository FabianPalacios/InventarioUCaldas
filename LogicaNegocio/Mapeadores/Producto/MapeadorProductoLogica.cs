using AccesoDeDatos.DbModel.Producto;
using LogicaNegocio.DTO.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Mapeadores.Producto
{
    public class MapeadorProductoLogica : MapeadorBaseLogica<ProductoDbModel, ProductoDTO>
    {
        public override ProductoDTO MapearTipo1Tipo2(ProductoDbModel entrada)
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
                Estado = entrada.Estado,

                NombreMarca = entrada.NombreMarca,
                NombreCategoria = entrada.NombreCategoria,
                NombreProducto = entrada.NombreProducto,
                NombreEspacio = entrada.NombreEspacio,
                DocumentoPersona = entrada.DocumentoPersona
            };
        }

        public override IEnumerable<ProductoDTO> MapearTipo1Tipo2(IEnumerable<ProductoDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override ProductoDbModel MapearTipo2Tipo1(ProductoDTO entrada)
        {
            return new ProductoDbModel()
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

        public override IEnumerable<ProductoDbModel> MapearTipo2Tipo1(IEnumerable<ProductoDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
