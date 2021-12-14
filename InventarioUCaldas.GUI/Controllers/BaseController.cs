using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioUCaldas.GUI.Controllers
{
    public class BaseController : Controller
    {
        public bool VerificarSession()
        {
            return Session.Count > 0;
        }
    }
}