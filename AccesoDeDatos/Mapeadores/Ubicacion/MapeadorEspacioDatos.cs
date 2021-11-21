using AccesoDeDatos.DbModel.Ubicacion;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Ubicacion
{
    public class MapeadorEspacioDatos : MapeadorBaseDatos<tb_espacio, EspacioDbModel>
    {
        public override EspacioDbModel MapearTipo1Tipo2(tb_espacio entrada)
        {
            return new EspacioDbModel()
            {
                Id = entrada.id,
                Nombre = entrada.nombre,
                IdPiso = entrada.id_piso,
                NombrePiso = entrada.tb_piso.nombre
            };
        }

        public override IEnumerable<EspacioDbModel> MapearTipo1Tipo2(IEnumerable<tb_espacio> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_espacio MapearTipo2Tipo1(EspacioDbModel entrada)
        {
            return new tb_espacio()
            {
                id = entrada.Id,
                nombre = entrada.Nombre,
                id_piso = entrada.IdPiso
            };
        }

        public override IEnumerable<tb_espacio> MapearTipo2Tipo1(IEnumerable<EspacioDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
