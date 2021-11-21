using AccesoDeDatos.DbModel.Ubicacion;
using LogicaNegocio.DTO.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Mapeadores.Ubicacion
{
    public class MapeadorSedeLogica: MapeadorBaseLogica<SedeDbModel, SedeDTO>
    {
        public override SedeDTO MapearTipo1Tipo2(SedeDbModel entrada)
        {
            return new SedeDTO()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                Direccion = entrada.Direccion
            };
        }

        public override IEnumerable<SedeDTO> MapearTipo1Tipo2(IEnumerable<SedeDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override SedeDbModel MapearTipo2Tipo1(SedeDTO entrada)
        {
            return new SedeDbModel()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                Direccion = entrada.Direccion
            };
        }

        public override IEnumerable<SedeDbModel> MapearTipo2Tipo1(IEnumerable<SedeDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
