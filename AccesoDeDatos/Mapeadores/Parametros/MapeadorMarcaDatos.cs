using AccesoDeDatos.DbModel.Parametros;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Parametros
{
    public class MapeadorMarcaDatos : MapeadorBaseDatos<tb_marca, MarcaDbModel>
    {
        public override MarcaDbModel MapearTipo1Tipo2(tb_marca entrada)
        {
            return new MarcaDbModel()
            {
                Id = entrada.id,
                Nombre = entrada.nombre
            };
        }

        public override IEnumerable<MarcaDbModel> MapearTipo1Tipo2(IEnumerable<tb_marca> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_marca MapearTipo2Tipo1(MarcaDbModel entrada)
        {
            return new tb_marca()
            {
                id = entrada.Id,
                nombre = entrada.Nombre
            };
        }

        public override IEnumerable<tb_marca> MapearTipo2Tipo1(IEnumerable<MarcaDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
