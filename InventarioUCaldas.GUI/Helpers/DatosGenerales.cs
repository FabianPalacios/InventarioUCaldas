using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Helpers
{
    public static class DatosGenerales
    {
        public static int RegistrosPorPagina = Int32.Parse(ConfigurationManager.AppSettings["registrosPorPagina"].ToString());
        public static String RutaArchivosProducto   = ConfigurationManager.AppSettings["rutaArchivosProducto"].ToString();
        public static String RutaMostrarArchivosProducto = ConfigurationManager.AppSettings["rutaMostrarArchivosProducto"].ToString();


    }
}