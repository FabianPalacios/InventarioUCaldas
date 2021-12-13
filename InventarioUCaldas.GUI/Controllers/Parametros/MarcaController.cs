    using InventarioUCaldas.GUI.Helpers;
using InventarioUCaldas.GUI.Mapeadores.Parametros;
using InventarioUCaldas.GUI.Mapeadores.Producto;
using InventarioUCaldas.GUI.Models.Parametros;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LogicaNegocio.DTO.Parametros;
using LogicaNegocio.Implementacion.Parametros;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InventarioUCaldas.GUI.Controllers.Parametros
{
    public class MarcaController : Controller
    {
        private ImplMarcaLogica logica = new ImplMarcaLogica();

        public ActionResult Index(string filtro,int? page)
        {
            
            if (filtro == null)
            {
                filtro = "";
            }
            
            int numPagina = page ?? 1;
            int totalRegistros;
            int registrosPorPagina = DatosGenerales.RegistrosPorPagina;
            IEnumerable<MarcaDTO> listaDatos = logica.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorMarcaGUI mapper = new MapeadorMarcaGUI();
            IEnumerable<ModeloMarcaGUI> listaGUI = mapper.MapearTipo1Tipo2(listaDatos);

            //var registrosPagina = listaGUI.ToPagedList(numPagina, registrosPorPagina);
            var listaPagina = new StaticPagedList<ModeloMarcaGUI>(listaGUI, numPagina, registrosPorPagina, totalRegistros);
            return View(listaPagina);
        }

        // GET: Marca/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            MarcaDTO MarcaDTO = logica.BuscarRegistro(id.Value);
            if (MarcaDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorMarcaGUI mapper = new MapeadorMarcaGUI();
            ModeloMarcaGUI modelo = mapper.MapearTipo1Tipo2(MarcaDTO);
            return View(modelo);
        }

        // GET: Marca/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] ModeloMarcaGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorMarcaGUI mapper = new MapeadorMarcaGUI();
                MarcaDTO MarcaDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.GuardarRegistro(MarcaDTO);
                return RedirectToAction("Index");
            }

            return View(modelo);
        }

        // GET: Marca/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarcaDTO MarcaDTO = logica.BuscarRegistro(id.Value);
            if (MarcaDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorMarcaGUI mapper = new MapeadorMarcaGUI();
            ModeloMarcaGUI modelo = mapper.MapearTipo1Tipo2(MarcaDTO);
            return View(modelo);
        }

        // POST: Marca/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] ModeloMarcaGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorMarcaGUI mapper = new MapeadorMarcaGUI();
                MarcaDTO MarcaDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.EditarRegistro(MarcaDTO);
                return RedirectToAction("Index");
            }
            return View(modelo);
        }

        // GET: Marca/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarcaDTO MarcaDTO = logica.BuscarRegistro(id.Value);
            if (MarcaDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorMarcaGUI mapper = new MapeadorMarcaGUI();
            ModeloMarcaGUI modelo = mapper.MapearTipo1Tipo2(MarcaDTO);
            return View(modelo);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool respuesta = logica.EliminarRegistro(id);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                MarcaDTO MarcaDTO = logica.BuscarRegistro(id);
                if (MarcaDTO == null)
                {
                    return HttpNotFound();
                }
                MapeadorMarcaGUI mapper = new MapeadorMarcaGUI();
                ViewBag.mensaje = Mensaje.mensajeErrorEliminar;
                ModeloMarcaGUI modelo = mapper.MapearTipo1Tipo2(MarcaDTO);     
                return View("Delete", modelo);
            }

        }

        public FileStreamResult Print()
        {
            DateTime hoy = DateTime.Now;
            string fecha_creacion = string.Format("{0}_{1}_{2}_{3}", hoy.Day, hoy.Hour, hoy.Minute, hoy.Millisecond);
            string nombre_archivo = string.Concat("marcas_" + fecha_creacion + ".pdf");
            string ruta = Server.MapPath("~/pdfReports/Marcas/" + nombre_archivo);
            MapeadorMarcaGUI mapeador = new MapeadorMarcaGUI();
            IEnumerable<ModeloMarcaGUI> listaDatos = mapeador.MapearTipo1Tipo2(logica.ListarRegistrosReporte());

            FabricaArchivosPdf fabrica = new FabricaArchivosPdf();
            bool archivoCreado = fabrica.CrearListadoMarcasEnPdf(ruta, "Listado Marcas", listaDatos);


            if(archivoCreado)
            {
                var fileStream = new FileStream(ruta,
                                                FileMode.Open,
                                                FileAccess.Read
                                                );
                var fsResult = new FileStreamResult(fileStream, "application/pdf");
                return fsResult;
            }
            else
            {
                throw new Exception("Error leyendo el archivo");
            }
        }
    }
}