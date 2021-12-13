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
using LogicaNegocio.Services;
using Newtonsoft.Json.Linq;

namespace InventarioUCaldas.GUI.Controllers.Seguridad
{
    public class UserController : BaseController
    {

        private UserImplController capaNegocio = new UserImplController();
        private RoleImplController capaNegocioRole = new RoleImplController();

        // GET: User
        public ActionResult Index(string filter = "")
        {
            /*
            if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }
            */
            UserModelMapper mapper = new UserModelMapper();
            IEnumerable<UserModel> roleList = mapper.MapearTipo1Tipo2(capaNegocio.RecordList(filter));
            return View(roleList);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            /*
            if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            return View();
        }

        // POST: User/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Lastname,Document,Cellphone,Email")] UserModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserInSessionId = 1;
                UserModelMapper mapper = new UserModelMapper();
                UserDTO dto = mapper.MapearTipo2Tipo1(model);
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

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            /*
            if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDTO dto = capaNegocio.RecordSearch(id.Value);
            if (dto == null)
            {
                return HttpNotFound();
            }
            UserModelMapper mapper = new UserModelMapper();
            UserModel model = mapper.MapearTipo1Tipo2(dto);

            return View(model);
        }

        // POST: User/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Lastname,Document,Cellphone,Email")] UserModel model)
        {
            if (ModelState.IsValid)
            {
                UserModelMapper mapper = new UserModelMapper();
                UserDTO dto = mapper.MapearTipo2Tipo1(model);
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
        private ActionResult ProcessResponse(int response, UserModel model)
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


        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            /*
            if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDTO dto = capaNegocio.RecordSearch(id.Value);
            if (dto == null)
            {
                return HttpNotFound();
            }
            UserModelMapper mapper = new UserModelMapper();
            UserModel model = mapper.MapearTipo1Tipo2(dto);
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id,Name,Removed,Description")] UserModel model)
        {
            UserModelMapper mapper = new UserModelMapper();
            UserDTO dto = mapper.MapearTipo2Tipo1(model);
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

        public ActionResult Roles(int? id)
        {
            /*
            if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<RoleDTO> dtoList = capaNegocio.RecordListByUser(id.Value);
            if (dtoList == null)
            {
                return HttpNotFound();
            }
            RoleModelMapper mapper = new RoleModelMapper();
            IEnumerable<RoleModel> roleList = mapper.MapearTipo1Tipo2(dtoList);
            var selectedList = roleList.Where(x => x.IsSelectedByUser).Select(x => x.Id).ToList();
            UserRoleModel model = new UserRoleModel()
            {
                UserId = id.Value,
                RoleList = roleList,
                SelectedRoles = String.Join(",", selectedList)
            };

            return View(model);
        }

        // POST: User/Roles/5
        [HttpPost, ActionName("Roles")]
        [ValidateAntiForgeryToken]
        public ActionResult Roles([Bind(Include = "UserId,SelectedRoles")] UserRoleModel model)
        {
            List<int> roleList = new List<int>();
            foreach (string roleId in model.SelectedRoles.Split(','))
            {
                roleList.Add(int.Parse(roleId));
            }
            bool response = capaNegocio.AssignRoles(roleList, model.UserId);
            if (response)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            //validando recaptacha de google
            var response = Request["g-recaptcha-response"];
            string secretKey = "6LexxrMaAAAAALnJK8WwntzE4mOhtQQgikSE5NtQ";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";

            /*
            if (this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            return View();
        }

        // POST: User/Login/5
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult IdentifyUser(LoginModel model)
        {
            UserDTO dto = new UserDTO()
            {
                Email = model.UserName,
                Password = model.Password,
                CurrentDate = DateTime.Now
            };
            UserDTO login = capaNegocio.Login(dto);
            if (login == null)
            {
                ViewBag.Message = Messages.ErrorMessage;
                return View(model);
            }
            else
            {
                Session["username"] = model.UserName;
                Session["token"] = login.Token;
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {
            /*
            if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            Session.Remove("username");
            Session.Remove("token");
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult PasswordReset(PasswordResetModel model)
        {
            return View();
        }


        [HttpPost, ActionName("PasswordReset")]
        public ActionResult SendEmailResetPass([Bind(Include = "UserName")] PasswordResetModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO dto = new UserDTO()
                {
                    Email = model.UserName
                };
                string Email = dto.Email.ToString();
                int response = capaNegocio.PasswordReset(Email);
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

        public ActionResult ChangePassword(PasswordResetModel model)
        {
            /*
            if (!this.VerificarSession())
            {
                return RedirectToAction("Index", "Home");
            }*/
            return View();
        }

        [HttpPost, ActionName("ChangePassword")]
        public ActionResult ChangePass([Bind(Include = "Password, NewPassword")] ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                SeguridadUdCEntities db = new SeguridadUdCEntities();
                var record = db.SEC_USER.Where(x => x.USER_PASSWORD == model.Password).FirstOrDefault();
                if (record != null)
                {
                    int id = record.ID;
                    Encrypt enc = new Encrypt();
                    string np = model.NewPassword;
                    string newPass = enc.CreateMD5(np);
                    string oldPass = model.Password;
                    int response = capaNegocio.ChangePassword(oldPass, newPass, id);

                    UserDTO dto = new UserDTO()
                    {
                        Password = newPass
                    };

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
            }
            return View(model);
        }
    }
}
