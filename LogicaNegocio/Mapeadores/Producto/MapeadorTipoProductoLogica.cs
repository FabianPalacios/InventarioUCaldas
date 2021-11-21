using AccesoDeDatos.DbModel.Producto;
using LogicaNegocio.DTO.Producto;
using System.Collections.Generic;

namespace LogicaNegocio.Mapeadores.Producto
{
    public class MapeadorTipoProductoLogica : MapeadorBaseLogica<TipoProductoDbModel, TipoProductoDTO>
    {
        public override TipoProductoDTO MapearTipo1Tipo2(TipoProductoDbModel entrada)
        {
            return new TipoProductoDTO()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre
            };
        }

        public override IEnumerable<TipoProductoDTO> MapearTipo1Tipo2(IEnumerable<TipoProductoDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override TipoProductoDbModel MapearTipo2Tipo1(TipoProductoDTO entrada)
        {
            return new TipoProductoDbModel()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre
            };
        }

        public override IEnumerable<TipoProductoDbModel> MapearTipo2Tipo1(IEnumerable<TipoProductoDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
