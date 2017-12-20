using DocumentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            string UsuarioAD = Util.GetUserAD();
            //UsuarioAD = UsuarioAD.Split('\\').GetValue(1).ToString(); // comentar para pruebas
            UsuarioAD = "djaimes"; // descomentar para pruebas

            UsuarioLoginViewModel UsuarioActual = new UsuarioLoginViewModel();

            UsuarioActual = Util.GetUser(UsuarioAD);
            Session.Add("objUsuario", UsuarioActual);
            return View(UsuarioActual);
        }

        public ActionResult Login()
        {
            Session.Remove("objUsuario");
            LoginViewModel usuarioLogin = new LoginViewModel();
            return View(usuarioLogin);
        }

        public ActionResult LoginIniciar(LoginViewModel usuarioLogin)
        {
            string UsuarioAD = usuarioLogin.NombreUsuario;
            UsuarioLoginViewModel UsuarioActual = new UsuarioLoginViewModel();

            UsuarioActual = Util.GetUser(UsuarioAD);
            Session.Add("objUsuario", UsuarioActual);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Ingresar()
        {

            return RedirectToAction("Index","Home");
        }

        public ActionResult LogOff()
        {
            Session.Remove("objUsuario");
            return RedirectToAction("Index","Login");
        }

    }
}