using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Helpers
{
    public static class Mensaje
    {
        public static String mensajeErrorEliminar = ConfigurationManager.AppSettings["MensajeErrorAlEliminar"];

    }
}