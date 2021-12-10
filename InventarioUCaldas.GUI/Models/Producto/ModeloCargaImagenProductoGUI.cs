using InventarioUCaldas.GUI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Producto
{
    public class ModeloCargaImagenProductoGUI
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private  HttpPostedFileBase archivos;

        [Required]
        public HttpPostedFileBase Archivos
        {
            get { return archivos; }
            set { archivos = value; }
        }

        private IEnumerable<ModeloFotoProductoGUI> listadoImagenesProducto;

        public IEnumerable<ModeloFotoProductoGUI> ListadoImagenesProducto
        {
            get { return listadoImagenesProducto; }
            set { listadoImagenesProducto = value; }
        }

        private String rutaImagenProducto = DatosGenerales.RutaMostrarArchivosProducto;

        public String RutaImagenProducto
        {
            get { return rutaImagenProducto; }
        }



    }
}