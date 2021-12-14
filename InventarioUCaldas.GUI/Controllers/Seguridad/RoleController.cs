using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccesoDeDatos.ModeloDeDatos;
using InventarioUCaldas.GUI.Helpers;
using InventarioUCaldas.GUI.Mapeadores.Seguridad;
using InventarioUCaldas.GUI.Models.Seguridad;
using LogicaNegocio.DTO.Seguridad;
using LogicaNegocio.Implementacion.Seguridad;

namespace InventarioUCaldas.GUI.Controllers.Seguridad
{
    public class RoleController : Controller
    {

        private RoleImplController capaNegocio = new RoleImplController();

        // GET: Role
        public ActionResult Index(string filter = "")
        {
            RoleModelMapper mapper = new RoleModelMapper();
            IEnumerable<RoleModel> roleList = mapper.MapearTipo1Tipo2(capaNegocio.RecordList(filter));
            return View(roleList);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] RoleModel model)
        {
            if (ModelState.IsValid)
            {
                RoleModelMapper mapper = new RoleModelMapper();
                RoleDTO dto = mapper.MapearTipo2Tipo1(model);
                int response = capaNegocio.RecordCreation(dto);
                switch (response)
                {
                    case 1:
                        return RedirectToAction("Index");
                    case 2:
                        ViewBag.Message = Messages.ExceptionMessage;
                        return View(model);
                    case 3:
                        ViewBag.Message = Messages.alreadyExistMessage;
                        return View(model);
                }
            }

            return View(model);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleDTO dto = capaNegocio.RecordSearch(id.Value);
            if (dto == null)
            {
                return HttpNotFound();
            }
            RoleModelMapper mapper = new RoleModelMapper();
            RoleModel model = mapper.MapearTipo1Tipo2(dto);

            return View(model);
        }

        // POST: Role/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Removed,Description")] RoleModel model)
        {
            if (ModelState.IsValid)
            {
                RoleModelMapper mapper = new RoleModelMapper();
                RoleDTO dto = mapper.MapearTipo2Tipo1(model);
                int response = capaNegocio.RecordUpdate(dto);
                //this.ProcessResponse(response, model);
                switch (response)
                {
                    case 1:
                        return RedirectToAction("Index");
                    case 2:
                        ViewBag.Message = Messages.ExceptionMessage;
                        return View(model);
                    case 3:
                        ViewBag.Message = Messages.alreadyExistMessage;
                        return View(model);
                }

            }
            return View(model);
        }

        /// <summary>
        /// Procesamiento de las respuestas
        /// </summary>
        /// <param name="response">Representa la respuesta</param>
        /// <param name="model">Representa un objeto con informacion del rol</param>
        /// <returns>1: Ok, 2: Excepción, 3:Ya existe</returns>
        private ActionResult ProcessResponse(int response, RoleModel model)
        {
            switch (response)
            {
                case 1:
                    return RedirectToAction("Index");
                case 2:
                    ViewBag.Message = Messages.ExceptionMessage;
                    return View(model);
                case 3:
                    ViewBag.Message = Messages.alreadyExistMessage;
                    return View(model);
            }
            return RedirectToAction("Index");
        }


        // GET: Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleDTO dto = capaNegocio.RecordSearch(id.Value);
            if (dto == null)
            {
                return HttpNotFound();
            }
            RoleModelMapper mapper = new RoleModelMapper();
            RoleModel model = mapper.MapearTipo1Tipo2(dto);
            return View(model);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id,Name,Removed,Description")] RoleModel model)
        {
            RoleModelMapper mapper = new RoleModelMapper();
            RoleDTO dto = mapper.MapearTipo2Tipo1(model);
            int response = capaNegocio.RecordRemove(dto);
            switch (response)
            {
                case 1:
                    return RedirectToAction("Index");
                case 2:
                    ViewBag.Message = Messages.ExceptionMessage;
                    return View(model);
                case 3:
                    ViewBag.Message = Messages.alreadyExistMessage;
                    return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Forms(int? id)
        {
            /*if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<FormDTO> dtoList = capaNegocio.RecordFormListByRole(id.Value);
            if (dtoList == null)
            {
                return HttpNotFound();
            }
            FormModelMapper mapper = new FormModelMapper();
            IEnumerable<FormModel> formList = mapper.MapearTipo1Tipo2(dtoList);
            var selectedList = formList.Where(x => x.IsSelectedByUser).Select(x => x.Id).ToList();

            FormsRoleModel model = new FormsRoleModel()
            {
                RoleId = id.Value,
                FormsList = formList,
                SelectedForms = String.Join(",", selectedList)
            };

            return View(model);
        }

        [HttpPost, ActionName("Forms")]
        [ValidateAntiForgeryToken]
        public ActionResult Forms([Bind(Include = "RoleId,SelectedForms")] FormsRoleModel model)
        {
            List<int> formsList = new List<int>();
            foreach (string formId in model.SelectedForms.Split(','))
            {
                formsList.Add(int.Parse(formId));
            }

            bool response = capaNegocio.AssignForms(formsList, model.RoleId);
            if (response)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
