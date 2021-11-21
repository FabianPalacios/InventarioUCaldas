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
    public class ProductoController : Controller
    {
        private ImplProductoLogica logica = new ImplProductoLogica();

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
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloProductoGUI modelo)
        {
            if (ModelState.IsValid)
            {
                MapeadorProductoGUI mapper = new MapeadorProductoGUI();
                ProductoDTO ProductoDTO = mapper.MapearTipo2Tipo1(modelo);
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
        [ValidateAntiForgeryToken]
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
    }
}