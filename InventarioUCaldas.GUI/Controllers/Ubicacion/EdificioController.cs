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
    public class EdificioController : Controller
    {
        private ImplEdificioLogica logica = new ImplEdificioLogica();

        public ActionResult Index(string filtro, int? page)
        {
            if (filtro == null)
            {
                filtro = "";
            }
            int numPagina = page ?? 1;
            int totalRegistros;
            int registrosPorPagina = DatosGenerales.RegistrosPorPagina;
            IEnumerable<EdificioDTO> listaDatos = logica.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorEdificioGUI mapper = new MapeadorEdificioGUI();
            IEnumerable<ModeloEdificioGUI> listaGUI = mapper.MapearTipo1Tipo2(listaDatos);

            //var registrosPagina = listaGUI.ToPagedList(numPagina, registrosPorPagina);
            var listaPagina = new StaticPagedList<ModeloEdificioGUI>(listaGUI, numPagina, registrosPorPagina, totalRegistros);
            return View(listaPagina);
        }

        // GET: Edificio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            EdificioDTO EdificioDTO = logica.BuscarRegistro(id.Value);
            if (EdificioDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorEdificioGUI mapper = new MapeadorEdificioGUI();
            ModeloEdificioGUI modelo = mapper.MapearTipo1Tipo2(EdificioDTO);
            return View(modelo);
        }

        // GET: Edificio/Create
        public ActionResult Create()
        {
            ViewBag.IdSede = new SelectList(logica.ListarRegistrosSede(), "Id", "Nombre");
            return View();
        }

        // POST: Edificio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloEdificioGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorEdificioGUI mapper = new MapeadorEdificioGUI();
                EdificioDTO EdificioDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.GuardarRegistro(EdificioDTO);
                return RedirectToAction("Index");
            }

            return View(modelo);
        }

        // GET: Edificio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdificioDTO EdificioDTO = logica.BuscarRegistro(id.Value);
            if (EdificioDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorEdificioGUI mapper = new MapeadorEdificioGUI();
            ModeloEdificioGUI modelo = mapper.MapearTipo1Tipo2(EdificioDTO);

            ViewBag.IdSede = new SelectList(logica.ListarRegistrosSede(), "Id", "Nombre");
            return View(modelo);
        }

        // POST: Edificio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModeloEdificioGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorEdificioGUI mapper = new MapeadorEdificioGUI();
                EdificioDTO EdificioDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.EditarRegistro(EdificioDTO);
                return RedirectToAction("Index");
            }
            ViewBag.IdSede = new SelectList(logica.ListarRegistrosSede(), "Id", "Nombre");
            return View(modelo);
        }

        // GET: Edificio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdificioDTO EdificioDTO = logica.BuscarRegistro(id.Value);
            if (EdificioDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorEdificioGUI mapper = new MapeadorEdificioGUI();
            ModeloEdificioGUI modelo = mapper.MapearTipo1Tipo2(EdificioDTO);
            return View(modelo);
        }

        // POST: Edificio/Delete/5
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
                EdificioDTO EdificioDTO = logica.BuscarRegistro(id);
                if (EdificioDTO == null)
                {
                    return HttpNotFound();
                }
                MapeadorEdificioGUI mapper = new MapeadorEdificioGUI();
                ViewBag.mensaje = Mensaje.mensajeErrorEliminar;
                ModeloEdificioGUI modelo = mapper.MapearTipo1Tipo2(EdificioDTO);
                return View("Delete", modelo);
            }

        }
    }
}