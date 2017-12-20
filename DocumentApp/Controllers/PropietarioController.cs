using Document.BusinessEntity;
using Document.BusinessLogic;
using DocumentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentApp.Controllers
{
    public class PropietarioController : Controller
    {
        // GET: Propietario
        public ActionResult Buscar()
        {
            List<PropietarioViewModel> ResultadoBusqueda;
            ResultadoBusqueda = Util.ListaPropietario();

            return View(ResultadoBusqueda);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        public ActionResult Registrar(string propietario)
        {
            UsuarioLoginViewModel UsuarioActual;
            UsuarioActual = (UsuarioLoginViewModel)Session["objUsuario"];

            BEPropietario item = new BEPropietario();
            item.gls_propietario = propietario;
            item.aud_usr_ingreso = UsuarioActual.Codigo;

            BLPropietario oBLPropietario = new BLPropietario();
            int iResultado = oBLPropietario.Registrar(item);

            return RedirectToAction("Buscar","Propietario");
            //return Json(iResultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Actualizar(string code, string descripcion)
        {
            UsuarioLoginViewModel UsuarioActual;
            UsuarioActual = (UsuarioLoginViewModel)Session["objUsuario"];

            BEPropietario propietario = new BEPropietario();
            propietario.cod_propietario = Convert.ToInt16(code);
            propietario.gls_propietario = descripcion;
            propietario.aud_usr_modificacion = UsuarioActual.Codigo;

            BLPropietario oBLPropietario = new BLPropietario();
            int iResultado = oBLPropietario.Actualizar(propietario);

            return RedirectToAction("Buscar", "Propietario");
        }

        public JsonResult Eliminar(string code)
        {
            UsuarioLoginViewModel UsuarioActual;
            UsuarioActual = (UsuarioLoginViewModel)Session["objUsuario"];

            BEPropietario propietario = new BEPropietario();
            propietario.cod_propietario = Convert.ToInt16(code);
            propietario.aud_usr_modificacion = UsuarioActual.Codigo;

            BLPropietario oBLPropietario = new BLPropietario();
            int iResultado = oBLPropietario.Eliminar(propietario);

            return Json(iResultado, JsonRequestBehavior.AllowGet);
        }

    }
}