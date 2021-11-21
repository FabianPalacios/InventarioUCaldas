using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace InventarioUCaldas.GUI.Models.Producto
{
    public class ModeloProductoGUI
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string fechaRegistro;
        [DisplayName("Fecha Registro")]
        public string FechaRegistro
        {
            get { return fechaRegistro; }
            set { fechaRegistro = value; }
        }

        private int idMarca;

        public int IdMarca
        {
            get { return idMarca; }
            set { idMarca = value; }
        }

        private int idCategoria;

        public int IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        private int idTipoProducto;

        public int IdTipoProducto
        {
            get { return idTipoProducto; }
            set { idTipoProducto = value; }
        }

        private string serial;

        public string Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        private int idEspacio;

        public int IdEspacio
        {
            get { return idEspacio; }
            set { idEspacio = value; }
        }

        private int idPersona;

        public int IdPersona
        {
            get { return idPersona; }
            set { idPersona = value; }
        }

        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }





        private string nombreMarca;
        [DisplayName("Marca")]
        public string NombreMarca
        {
            get { return nombreMarca; }
            set { nombreMarca = value; }
        }

        private string nombreCategoria;
        [DisplayName("Categoria")]
        public string NombreCategoria
        {
            get { return nombreCategoria; }
            set { nombreCategoria = value; }
        }

        private string nombreProducto;
        [DisplayName("Tipo Producto")]
        public string NombreProducto
        {
            get { return nombreProducto; }
            set { nombreProducto = value; }
        }

        private string nombreEspacio;
        [DisplayName("Espacio")]
        public string NombreEspacio
        {
            get { return nombreEspacio; }
            set { nombreEspacio = value; }
        }

        private string documentoPersona;
        [DisplayName("Documento")]
        public string DocumentoPersona
        {
            get { return documentoPersona; }
            set { documentoPersona = value; }
        }
    }
}