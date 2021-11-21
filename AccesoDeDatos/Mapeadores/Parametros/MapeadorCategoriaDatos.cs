using AccesoDeDatos.DbModel.Parametros;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Parametros
{
    public class MapeadorCategoriaDatos: MapeadorBaseDatos<tb_categoria, CategoriaDbModel>
    {
        public override  CategoriaDbModel MapearTipo1Tipo2(tb_categoria entrada)
        {
            return new  CategoriaDbModel()
            {
                Id = entrada.id,
                Nombre = entrada.nombre
            };
        }

        public override IEnumerable< CategoriaDbModel> MapearTipo1Tipo2(IEnumerable<tb_categoria> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_categoria MapearTipo2Tipo1(CategoriaDbModel entrada)
        {
            return new tb_categoria()
            {
                id = entrada.Id,
                nombre = entrada.Nombre
            };
        }

        public override IEnumerable<tb_categoria> MapearTipo2Tipo1(IEnumerable<CategoriaDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
