using AccesoDeDatos.DbModel.Producto;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Producto
{
    public  class MapeadorProductoDatos : MapeadorBaseDatos<tb_producto, ProductoDbModel>
    {
        public override ProductoDbModel MapearTipo1Tipo2(tb_producto entrada)
        {
            return new ProductoDbModel()
            {
                Id = entrada.id,
                Nombre = entrada.nombre,
                FechaRegistro = entrada.fecha_registro,
                IdMarca = entrada.id_marca,
                IdCategoria = entrada.id_categoria,
                IdTipoProducto = entrada.id_tipo_producto,
                Serial = entrada.serial,
                IdEspacio = entrada.id_espacio,
                IdPersona = entrada.id_persona,
                Estado = entrada.estado,

                NombreMarca = entrada.tb_marca.nombre,
                NombreCategoria = entrada.tb_categoria.nombre,
                NombreProducto = entrada.tb_tipoProducto.nombre,
                NombreEspacio = entrada.tb_espacio.nombre,
                DocumentoPersona = entrada.tb_persona.documento

            };
        }

        public override IEnumerable<ProductoDbModel> MapearTipo1Tipo2(IEnumerable<tb_producto> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_producto MapearTipo2Tipo1(ProductoDbModel entrada)
        {
            return new tb_producto()
            {
                id = entrada.Id,
                nombre = entrada.Nombre,
                fecha_registro = entrada.FechaRegistro,
                id_marca = entrada.IdMarca,
                id_categoria = entrada.IdCategoria,
                id_tipo_producto = entrada.IdTipoProducto,
                serial = entrada.Serial,
                id_espacio = entrada.IdEspacio,
                id_persona = entrada.IdPersona,
                estado = entrada.Estado

            };
        }

        public override IEnumerable<tb_producto> MapearTipo2Tipo1(IEnumerable<ProductoDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
