using AccesoDeDatos.DbModel.Ubicacion;
using AccesoDeDatos.ModeloDeDatos;
using System.Collections.Generic;

namespace AccesoDeDatos.Mapeadores.Ubicacion
{
    public class MapeadorEdificioDatos : MapeadorBaseDatos<tb_edificio, EdificioDbModel>
    {
        public override EdificioDbModel MapearTipo1Tipo2(tb_edificio entrada)
        {
            return new EdificioDbModel()
            {
                Id = entrada.id,
                Nombre = entrada.nombre,
                Bloque = entrada.bloque,
                IdSede = entrada.id_sede,
                NombreSede = entrada.tb_sede.nombre
            };
        }

        public override IEnumerable<EdificioDbModel> MapearTipo1Tipo2(IEnumerable<tb_edificio> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override tb_edificio MapearTipo2Tipo1(EdificioDbModel entrada)
        {
            return new tb_edificio()
            {
                id = entrada.Id,
                nombre = entrada.Nombre,
                bloque = entrada.Bloque,
                id_sede = entrada.IdSede
            };
        }

        public override IEnumerable<tb_edificio> MapearTipo2Tipo1(IEnumerable<EdificioDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
