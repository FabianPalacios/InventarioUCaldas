using AccesoDeDatos.DbModel.Parametros;
using AccesoDeDatos.DbModel.Producto;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Producto
{
    public class MapeadorFotoProductoDatos : MapeadorBaseDatos<tb_foto, FotoProductoDbModel>
    {
        public override FotoProductoDbModel MapearTipo1Tipo2(tb_foto entrada)
        {
            return new FotoProductoDbModel()
            {
                Id = entrada.id,
                IdProducto = entrada.id_producto,
                NombreFoto = entrada.nombre
            };
        }

        public override IEnumerable<FotoProductoDbModel> MapearTipo1Tipo2(IEnumerable<tb_foto> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_foto MapearTipo2Tipo1(FotoProductoDbModel entrada)
        {
            return new tb_foto()
            {
                id  = entrada.Id,
                id_producto = entrada.IdProducto,
                nombre = entrada.NombreFoto
            };
        }

        public override IEnumerable<tb_foto> MapearTipo2Tipo1(IEnumerable<FotoProductoDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
