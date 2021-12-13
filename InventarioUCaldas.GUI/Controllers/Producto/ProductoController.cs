using InventarioUCaldas.GUI.Helpers;
using InventarioUCaldas.GUI.Mapeadores.Producto;
using InventarioUCaldas.GUI.Models.Producto;
using LogicaNegocio.DTO.Producto;
using LogicaNegocio.DTO.Persona;
using LogicaNegocio.Implementacion.Persona;
using LogicaNegocio.Implementacion.Producto;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace InventarioUCaldas.GUI.Controllers.Producto
{
    public class ProductoController : Controller
    {
        private ImplProductoLogica logica = new ImplProductoLogica();
        private ImplPersonaLogica logicaPersona = new ImplPersonaLogica();

        public ActionResult Index(int? page, String filtro = "")
        {
            int numPagina = page ?? 1;
            int totalRegistros;
            int registrosPorPagina = DatosGenerales.RegistrosPorPagina;
            IEnumerable<ProductoDTO> listaDatos = logica.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorProductoGUI mapper = new MapeadorProductoGUI();
            IEnumerable<ModeloProductoGUI> listaGUI = mapper.MapearTipo1Tipo2(listaDatos);

            //var registrosPagina = listaGUI.ToPagedList(numPagina, registrosPorPagina);
            var listaPagina = new StaticPagedList<ModeloProductoGUI>(listaGUI, numPagina, registrosPorPagina, totalRegistros);
            return View(listaPagina);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            ProductoDTO ProductoDTO = logica.BuscarRegistro(id.Value);
            if (ProductoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorProductoGUI mapper = new MapeadorProductoGUI();
            ModeloProductoGUI modelo = mapper.MapearTipo1Tipo2(ProductoDTO);
            return View(modelo);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.IdMarca = new SelectList(logica.ListarRegistrosMarca(), "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(logica.ListarRegistrosCategoria(), "Id", "Nombre");
            ViewBag.IdTipoProducto = new SelectList(logica.ListarRegistrosTipoProducto(), "Id", "Nombre");
            ViewBag.IdEspacio = new SelectList(logica.ListarRegistrosEspacio(), "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(logica.ListarRegistrosPersona(), "Id", "Documento");
            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ModeloProductoGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorProductoGUI mapper = new MapeadorProductoGUI();
                ProductoDTO ProductoDTO = mapper.MapearTipo2Tipo1(modelo);
                ProductoResponse(modelo);
                logica.GuardarRegistro(ProductoDTO);
                return RedirectToAction("Index");
            }

            return View(modelo);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoDTO ProductoDTO = logica.BuscarRegistro(id.Value);
            if (ProductoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorProductoGUI mapper = new MapeadorProductoGUI();
            ModeloProductoGUI modelo = mapper.MapearTipo1Tipo2(ProductoDTO);

            ViewBag.IdMarca = new SelectList(logica.ListarRegistrosMarca(), "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(logica.ListarRegistrosCategoria(), "Id", "Nombre");
            ViewBag.IdTipoProducto = new SelectList(logica.ListarRegistrosTipoProducto(), "Id", "Nombre");
            ViewBag.IdEspacio = new SelectList(logica.ListarRegistrosEspacio(), "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(logica.ListarRegistrosPersona(), "Id", "Documento");
            return View(modelo);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit( ModeloProductoGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorProductoGUI mapper = new MapeadorProductoGUI();
                ProductoDTO ProductoDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.EditarRegistro(ProductoDTO);
                return RedirectToAction("Index");
            }
            ViewBag.IdMarca = new SelectList(logica.ListarRegistrosMarca(), "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(logica.ListarRegistrosCategoria(), "Id", "Nombre");
            ViewBag.IdTipoProducto = new SelectList(logica.ListarRegistrosTipoProducto(), "Id", "Nombre");
            ViewBag.IdEspacio = new SelectList(logica.ListarRegistrosEspacio(), "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(logica.ListarRegistrosPersona(), "Id", "Documento");
            return View(modelo);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoDTO ProductoDTO = logica.BuscarRegistro(id.Value);
            if (ProductoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorProductoGUI mapper = new MapeadorProductoGUI();
            ModeloProductoGUI modelo = mapper.MapearTipo1Tipo2(ProductoDTO);
            return View(modelo);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            bool respuesta = logica.EliminarRegistro(id);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ProductoDTO ProductoDTO = logica.BuscarRegistro(id);
                if (ProductoDTO == null)
                {
                    return HttpNotFound();
                }
                MapeadorProductoGUI mapper = new MapeadorProductoGUI();
                ViewBag.mensaje = Mensaje.mensajeErrorEliminar;
                ModeloProductoGUI modelo = mapper.MapearTipo1Tipo2(ProductoDTO);
                return View("Delete", modelo);
            }

        }
        [HttpGet]
         public ActionResult SubirFoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeloCargaImagenProductoGUI modelo = CrearModeloCargarImagenProducto(id);
            return View(modelo);
        }

        private ModeloCargaImagenProductoGUI CrearModeloCargarImagenProducto(int? id)
        {
            IEnumerable<FotoProductoDTO> listaDto = logica.listarFotoProductoPorId(id.Value);
            MapeadorFotoProductoGUI mapeador = new MapeadorFotoProductoGUI();
            IEnumerable<ModeloFotoProductoGUI> listaFotos = mapeador.MapearTipo1Tipo2(listaDto);
            if(listaFotos == null)
            {
                listaFotos = new List<ModeloFotoProductoGUI>();
            }
            ModeloCargaImagenProductoGUI modelo = new ModeloCargaImagenProductoGUI()
            {
                Id = id.Value,
                ListadoImagenesProducto = listaFotos
            };
            return modelo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubirFoto(ModeloCargaImagenProductoGUI modelo)
        { 
            try
            {
                if(modelo.Archivos.ContentLength > 0)
                {
                    try
                    {
                        DateTime ahora = DateTime.Now;
                        string fecha_nombre = String.Format("{0}_{1}_{2}_{3}_{4}_{5}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Millisecond);
                        string nombreArchivo = String.Concat(fecha_nombre, "_", Path.GetFileName(modelo.Archivos.FileName));
                        string rutaCarpeta = DatosGenerales.RutaArchivosProducto;
                        string rutaCompletaArchivo = Path.Combine(Server.MapPath(rutaCarpeta), nombreArchivo);
                        modelo.Archivos.SaveAs(rutaCompletaArchivo);
                        FotoProductoDTO dto = new FotoProductoDTO() {
                            IdProducto = modelo.Id,
                            NombreFoto = nombreArchivo,
                        };
                        logica.GuardarNombreFoto(dto);

                        //Guardar nombre de archivo

                        ViewBag.UploadFileMessage = "Archivo cargado correctamente ";
                        ModeloCargaImagenProductoGUI modeloView = CrearModeloCargarImagenProducto(modelo.Id);  
                        return View(modeloView);
                    }
                    catch
                    {

                    }
                }
                ViewBag.UploadFileMessage = "Porfavor seleccione al menos un archivo por cargar ";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.UploadFileMessage = "Error al cargar el archivo. ";
                return View(); 
            }
        }

        public ActionResult EliminarFoto(int idFotoProducto, String nombreFotoProducto)
        {
            bool respuesta = logica.EliminarRegistroFoto(idFotoProducto);
            if (respuesta)
            {
                string rutaCarpeta = DatosGenerales.RutaArchivosProducto;
                string rutaCompletaArchivo = Path.Combine(Server.MapPath(rutaCarpeta), nombreFotoProducto);
                System.IO.File.Delete(rutaCompletaArchivo);
            }
            return RedirectToAction("Index");

        }

        public ActionResult ProductoResponse(ModeloProductoGUI modelo)
        {

            PersonaDTO personaDTO = logicaPersona.BuscarRegistro(modelo.IdPersona);
            
            if (personaDTO.Correo != null)
            {
                Console.WriteLine("Correo enviado a ", personaDTO.Correo);
                var email = personaDTO.Correo;
                String content = String.Format("INVENTARIO UCALDAS, " +
                "<br /> Inventario UNIVERSIDAD DE CALDAS " +
                "Le informamos los productos que tiene asocciados. <br/>" +
                " <ul>" +
                "<li> PRODUCTO : " +modelo.Nombre+"</li>" +
                "<li> FECHA     : " + modelo.FechaRegistro + "</li>" +
                "<li> MARCA     : " + modelo.NombreMarca + "</li>" +
                "<li> CATEGORIA : " + modelo.NombreCategoria + "</li>" +
                "<li> TIPO PRODUCTO : " + modelo.NombreProducto + "</li>" +
                "<li> SERIAL : " + modelo.Serial + "</li>" +
                "<li> ESPACIO : " + modelo.NombreEspacio + "</li>" +
                "</ul>" +
                "<br /> Cordial saludo, <br />" +
                "UNIVERSIDAD DE CALDAS.");
                string From = ConfigurationManager.AppSettings["EmailFromSendGrid"];
                new LogicaNegocio.Services.Notifications().SendEmail("Respuesta a solicitud", content, email, From);
            }
            return View(modelo);
        }


    }
}