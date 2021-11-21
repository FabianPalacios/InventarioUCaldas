using AccesoDeDatos.DbModel.Parametros;
using LogicaNegocio.DTO.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Mapeadores.Parametros
{
    public class MapeadorCategoriaLogica : MapeadorBaseLogica<CategoriaDbModel, CategoriaDTO>
    {
        public override CategoriaDTO MapearTipo1Tipo2(CategoriaDbModel entrada)
        {
            return new CategoriaDTO()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre
            };
        }

        public override IEnumerable<CategoriaDTO> MapearTipo1Tipo2(IEnumerable<CategoriaDbModel> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo1Tipo2(item);
            }
        }

        public override CategoriaDbModel MapearTipo2Tipo1(CategoriaDTO entrada)
        {
            return new CategoriaDbModel()
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre
            };
        }

        public override IEnumerable<CategoriaDbModel> MapearTipo2Tipo1(IEnumerable<CategoriaDTO> entrada)
        {
            foreach (var item in entrada)
            {
                yield return MapearTipo2Tipo1(item);
            }
        }
    }
}
