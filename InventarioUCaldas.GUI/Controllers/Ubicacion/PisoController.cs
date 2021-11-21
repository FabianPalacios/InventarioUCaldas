using InventarioUCaldas.GUI.Helpers;
using InventarioUCaldas.GUI.Mapeadores.Ubicacion;
using InventarioUCaldas.GUI.Models.Ubicacion;
using LogicaNegocio.DTO.Ubicacion;
using LogicaNegocio.Implementacion.Ubicacion;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InventarioUCaldas.GUI.Controllers.Ubicacion
{
    public class PisoController : Controller
    {
        private ImplPisoLogica logica = new ImplPisoLogica();

        public ActionResult Index(int? page, String filtro = "")
        {
            int numPagina = page ?? 1;
            int totalRegistros;
            int registrosPorPagina = DatosGenerales.RegistrosPorPagina;
            IEnumerable<PisoDTO> listaDatos = logica.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorPisoGUI mapper = new MapeadorPisoGUI();
            IEnumerable<ModeloPisoGUI> listaGUI = mapper.MapearTipo1Tipo2(listaDatos);

            //var registrosPagina = listaGUI.ToPagedList(numPagina, registrosPorPagina);
            var listaPagina = new StaticPagedList<ModeloPisoGUI>(listaGUI, numPagina, registrosPorPagina, totalRegistros);
            return View(listaPagina);
        }

        // GET: Piso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            PisoDTO PisoDTO = logica.BuscarRegistro(id.Value);
            if (PisoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorPisoGUI mapper = new MapeadorPisoGUI();
            ModeloPisoGUI modelo = mapper.MapearTipo1Tipo2(PisoDTO);
            return View(modelo);
        }

        // GET: Piso/Create
        public ActionResult Create()
        {
            ViewBag.IdEdificio = new SelectList(logica.ListarRegistrosEdificio(), "Id", "Nombre");
            return View();
        }

        // POST: Piso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloPisoGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorPisoGUI mapper = new MapeadorPisoGUI();
                PisoDTO PisoDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.GuardarRegistro(PisoDTO);
                return RedirectToAction("Index");
            }

            return View(modelo);
        }

        // GET: Piso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PisoDTO PisoDTO = logica.BuscarRegistro(id.Value);
            if (PisoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorPisoGUI mapper = new MapeadorPisoGUI();
            ModeloPisoGUI modelo = mapper.MapearTipo1Tipo2(PisoDTO);
            ViewBag.IdEdificio = new SelectList(logica.ListarRegistrosEdificio(), "Id", "Nombre");
            return View(modelo);
        }

        // POST: Piso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModeloPisoGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorPisoGUI mapper = new MapeadorPisoGUI();
                PisoDTO PisoDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.EditarRegistro(PisoDTO);
                return RedirectToAction("Index");
            }
            ViewBag.IdEdificio = new SelectList(logica.ListarRegistrosEdificio(), "Id", "Nombre");
            return View(modelo);
        }

        // GET: Piso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PisoDTO PisoDTO = logica.BuscarRegistro(id.Value);
            if (PisoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorPisoGUI mapper = new MapeadorPisoGUI();
            ModeloPisoGUI modelo = mapper.MapearTipo1Tipo2(PisoDTO);
            return View(modelo);
        }

        // POST: Piso/Delete/5
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
                PisoDTO PisoDTO = logica.BuscarRegistro(id);
                if (PisoDTO == null)
                {
                    return HttpNotFound();
                }
                MapeadorPisoGUI mapper = new MapeadorPisoGUI();
                ViewBag.mensaje = Mensaje.mensajeErrorEliminar;
                ModeloPisoGUI modelo = mapper.MapearTipo1Tipo2(PisoDTO);
                return View("Delete", modelo);
            }

        }
    }
}