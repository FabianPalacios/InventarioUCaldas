using InventarioUCaldas.GUI.Helpers;
using InventarioUCaldas.GUI.Mapeadores.Producto;
using InventarioUCaldas.GUI.Models.Producto;
using LogicaNegocio.DTO.Producto;
using LogicaNegocio.Implementacion.Producto;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InventarioUCaldas.GUI.Controllers.Producto
{
    public class TipoProductoController : Controller
    {
        private ImplTipoProductoLogica logica = new ImplTipoProductoLogica();

        public ActionResult Index(int? page, String filtro = "")
        {
            int numPagina = page ?? 1;
            int totalRegistros;
            int registrosPorPagina = DatosGenerales.RegistrosPorPagina;
            IEnumerable<TipoProductoDTO> listaDatos = logica.ListarRegistros(filtro, numPagina, registrosPorPagina, out totalRegistros);
            MapeadorTipoProductoGUI mapper = new MapeadorTipoProductoGUI();
            IEnumerable<ModeloTipoProductoGUI> listaGUI = mapper.MapearTipo1Tipo2(listaDatos);

            //var registrosPagina = listaGUI.ToPagedList(numPagina, registrosPorPagina);
            var listaPagina = new StaticPagedList<ModeloTipoProductoGUI>(listaGUI, numPagina, registrosPorPagina, totalRegistros);
            return View(listaPagina);
        }

        // GET: Marca/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            TipoProductoDTO TipoProductoDTO = logica.BuscarRegistro(id.Value);
            if (TipoProductoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorTipoProductoGUI mapper = new MapeadorTipoProductoGUI();
            ModeloTipoProductoGUI modelo = mapper.MapearTipo1Tipo2(TipoProductoDTO);
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
        public ActionResult Create([Bind(Include = "Id,Nombre")] ModeloTipoProductoGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorTipoProductoGUI mapper = new MapeadorTipoProductoGUI();
                TipoProductoDTO TipoProductoDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.GuardarRegistro(TipoProductoDTO);
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
            TipoProductoDTO TipoProductoDTO = logica.BuscarRegistro(id.Value);
            if (TipoProductoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorTipoProductoGUI mapper = new MapeadorTipoProductoGUI();
            ModeloTipoProductoGUI modelo = mapper.MapearTipo1Tipo2(TipoProductoDTO);
            return View(modelo);
        }

        // POST: Marca/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] ModeloTipoProductoGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorTipoProductoGUI mapper = new MapeadorTipoProductoGUI();
                TipoProductoDTO TipoProductoDTO = mapper.MapearTipo2Tipo1(modelo);
                logica.EditarRegistro(TipoProductoDTO);
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
            TipoProductoDTO TipoProductoDTO = logica.BuscarRegistro(id.Value);
            if (TipoProductoDTO == null)
            {
                return HttpNotFound();
            }
            MapeadorTipoProductoGUI mapper = new MapeadorTipoProductoGUI();
            ModeloTipoProductoGUI modelo = mapper.MapearTipo1Tipo2(TipoProductoDTO);
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
                TipoProductoDTO TipoProductoDTO = logica.BuscarRegistro(id);
                if (TipoProductoDTO == null)
                {
                    return HttpNotFound();
                }
                MapeadorTipoProductoGUI mapper = new MapeadorTipoProductoGUI();
                ViewBag.mensaje = Mensaje.mensajeErrorEliminar;
                ModeloTipoProductoGUI modelo = mapper.MapearTipo1Tipo2(TipoProductoDTO);
                return View("Delete", modelo);
            }

        }
    }
}