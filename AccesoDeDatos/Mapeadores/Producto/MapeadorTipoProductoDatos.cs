using AccesoDeDatos.DbModel.Producto;
using AccesoDeDatos.ModeloDeDatos;
using System.Collections.Generic;

namespace AccesoDeDatos.Mapeadores.Producto
{
    public class MapeadorTipoProductoDatos : MapeadorBaseDatos<tb_tipoProducto, TipoProductoDbModel>
    {
        public override TipoProductoDbModel MapearTipo1Tipo2(tb_tipoProducto entrada)
        {
            return new TipoProductoDbModel()
            {
                Id = entrada.id,
                Nombre = entrada.nombre
            };
        }

        public override IEnumerable<TipoProductoDbModel> MapearTipo1Tipo2(IEnumerable<tb_tipoProducto> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_tipoProducto MapearTipo2Tipo1(TipoProductoDbModel entrada)
        {
            return new tb_tipoProducto()
            {
                id = entrada.Id,
                nombre = entrada.Nombre
            };
        }

        public override IEnumerable<tb_tipoProducto> MapearTipo2Tipo1(IEnumerable<TipoProductoDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
