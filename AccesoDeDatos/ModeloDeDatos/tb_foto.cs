//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoDeDatos.ModeloDeDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_foto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int id_producto { get; set; }
    
        public virtual tb_producto tb_producto { get; set; }
    }
}
