using AccesoDeDatos.DbModel.Parametros;
using AccesoDeDatos.DbModel.Producto;
using LogicaNegocio.DTO.Parametros;
using LogicaNegocio.DTO.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Mapeadores.Pr
{
    public class MapeadorFotoProductoLogica : MapeadorBaseLogica<FotoProductoDbModel, FotoProductoDTO>
    {
        public override FotoProductoDTO MapearTipo1Tipo2(FotoProductoDbModel entrada)
        {
            return new FotoProductoDTO()
            {
                Id = entrada.Id,
                IdProducto = entrada.IdProducto,
                NombreFoto = entrada.NombreFoto
            };
        }

        public override IEnumerable<FotoProductoDTO> MapearTipo1Tipo2(IEnumerable<FotoProductoDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override FotoProductoDbModel MapearTipo2Tipo1(FotoProductoDTO entrada)
        {
            return new FotoProductoDbModel()
            {
                Id = entrada.Id,
                IdProducto = entrada.IdProducto,
                NombreFoto = entrada.NombreFoto
            };
        }

        public override IEnumerable<FotoProductoDbModel> MapearTipo2Tipo1(IEnumerable<FotoProductoDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
