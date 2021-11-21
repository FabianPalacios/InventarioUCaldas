using AccesoDeDatos.DbModel.Ubicacion;
using AccesoDeDatos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Mapeadores.Ubicacion
{
    public class MapeadorPisoDatos: MapeadorBaseDatos<tb_piso, PisoDbModel>
    {
        public override PisoDbModel MapearTipo1Tipo2(tb_piso entrada)
        {
            return new PisoDbModel()
            {
                Id = entrada.id,
                Nombre = entrada.nombre,
                IdEdificio = entrada.id_edificio,
                NombreEdificio = entrada.tb_edificio.nombre
            };
        }

        public override IEnumerable<PisoDbModel> MapearTipo1Tipo2(IEnumerable<tb_piso> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_piso MapearTipo2Tipo1(PisoDbModel entrada)
        {
            return new tb_piso()
            {
                id = entrada.Id,
                nombre = entrada.Nombre,
                id_edificio = entrada.IdEdificio 
            };
        }

        public override IEnumerable<tb_piso> MapearTipo2Tipo1(IEnumerable<PisoDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
