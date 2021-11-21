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
    public class EspacioController : Controller
    {
        private ImplEspacioLogica logica = new ImplEspacioLogica();

        public ActionResult Index(int? page, String filtro = "")
        {
            int numPagina = page ?? 1;
            int totalRegistros;
            int registrosPorPagina = DatosGenerales.RegistrosPorPagina;
            IEnumerable<EspacioDTO> listaDatos = logica.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorEspacioGUI mapper = new MapeadorEspacioGUI();
            IEnumerable<ModeloEspacioGUI> listaGUI = mapper.MapearTipo1Tipo2(listaDatos);

            //var registrosPagina = listaGUI.ToPagedList(numPagina, registrosPorPagina);
            var listaPagina = new StaticPagedList<ModeloEspacioGUI>(listaGUI, numPagina, registrosPorPagina, totalRegistros);
            return View(listaPagina);
        }

        // GET: Espacio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            EspacioDTO EspacioDTO = logica.BuscarRegistro(id.Value);
            if (EspacioDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorEspacioGUI mapper = new MapeadorEspacioGUI();
            ModeloEspacioGUI modelo = mapper.MapearTipo1Tipo2(EspacioDTO);
            return View(modelo);
        }

        // GET: Espacio/Create
        public ActionResult Create()
        {
            ViewBag.IdPiso = new SelectList(logica.ListarRegistrosPiso(), "Id", "Nombre");
            return View();
        }

        // POST: Espacio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloEspacioGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorEspacioGUI mapper = new MapeadorEspacioGUI();
                EspacioDTO EspacioDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.GuardarRegistro(EspacioDTO);
                return RedirectToAction("Index");
            }

            return View(modelo);
        }

        // GET: Espacio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspacioDTO EspacioDTO = logica.BuscarRegistro(id.Value);
            if (EspacioDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorEspacioGUI mapper = new MapeadorEspacioGUI();
            ModeloEspacioGUI modelo = mapper.MapearTipo1Tipo2(EspacioDTO);

            ViewBag.IdPiso = new SelectList(logica.ListarRegistrosPiso(), "Id", "Nombre");
            return View(modelo);
        }

        // POST: Espacio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModeloEspacioGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorEspacioGUI mapper = new MapeadorEspacioGUI();
                EspacioDTO EspacioDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.EditarRegistro(EspacioDTO);
                return RedirectToAction("Index");
            }
            ViewBag.IdPiso = new SelectList(logica.ListarRegistrosPiso(), "Id", "Nombre");
            return View(modelo);
        }

        // GET: Espacio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspacioDTO EspacioDTO = logica.BuscarRegistro(id.Value);
            if (EspacioDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorEspacioGUI mapper = new MapeadorEspacioGUI();
            ModeloEspacioGUI modelo = mapper.MapearTipo1Tipo2(EspacioDTO);
            return View(modelo);
        }

        // POST: Espacio/Delete/5
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
                EspacioDTO EspacioDTO = logica.BuscarRegistro(id);
                if (EspacioDTO == null)
                {
                    return HttpNotFound();
                }
                MapeadorEspacioGUI mapper = new MapeadorEspacioGUI();
                ViewBag.mensaje = Mensaje.mensajeErrorEliminar;
                ModeloEspacioGUI modelo = mapper.MapearTipo1Tipo2(EspacioDTO);
                return View("Delete", modelo);
            }

        }
    }
}