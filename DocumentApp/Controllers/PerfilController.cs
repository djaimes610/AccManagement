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
    public class PerfilController : Controller
    {
        // GET: Perfil
        [HttpGet]
        public ActionResult MantenimientoPerfil(int? page)
        {
            
            PerfilViewModel perfiles = new PerfilViewModel();
            var listaPerfiles = new List<Perfil>();
            Perfil perfil;

            List<BEPerfil> olistaPerfiles;
            BLPerfil oBlperfil = new BLPerfil();

            olistaPerfiles = oBlperfil.ListarPerfiles();
            int iCorrelativo = 0;
            var pager = new Pager(olistaPerfiles.Count(), page);

            foreach (BEPerfil item in olistaPerfiles)
            {
                perfil = new Perfil();
                iCorrelativo += 1;
                perfil.Nro = iCorrelativo;
                perfil.Id = item.cod_perfil;
                perfil.Descripcion = item.gls_perfil;
                perfil.Estado = item.cod_estado_registro;
                listaPerfiles.Add(perfil);
            }

            perfiles.Items = listaPerfiles.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
            perfiles.Pager = pager;
             
            return View(perfiles);
        }

        public ActionResult Actualizar(string id, string descripcion)
        {
            UsuarioLoginViewModel UsuarioActual;
            UsuarioActual = (UsuarioLoginViewModel)Session["objUsuario"];

            BEPerfil perfil = new BEPerfil();
            perfil.cod_perfil = Convert.ToInt16(id);
            perfil.gls_perfil = descripcion;
            perfil.aud_usr_modificacion = UsuarioActual.Codigo;

            BLPerfil oBLPropietario = new BLPerfil();
            int iResultado = oBLPropietario.Actualizar(perfil);

            return RedirectToAction("MantenimientoPerfil", "Perfil");
        }

        public JsonResult Eliminar(string id)
        {
            UsuarioLoginViewModel UsuarioActual;
            UsuarioActual = (UsuarioLoginViewModel)Session["objUsuario"];

            BEPerfil perfil = new BEPerfil();
            perfil.cod_perfil = Convert.ToInt16(id);
            perfil.aud_usr_modificacion = UsuarioActual.Codigo;

            BLPerfil oBLPerfil = new BLPerfil();
            int iResultado = oBLPerfil.Eliminar(perfil);

            return Json(iResultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Registrar(string perfil)
        {
            UsuarioLoginViewModel UsuarioActual;
            UsuarioActual = (UsuarioLoginViewModel)Session["objUsuario"];

            BEPerfil item = new BEPerfil();
            item.gls_perfil = perfil;
            item.aud_usr_ingreso = UsuarioActual.Codigo;

            BLPerfil oBLPropietario = new BLPerfil();
            int iResultado = oBLPropietario.Registrar(item);

            return RedirectToAction("MantenimientoPerfil", "Perfil");
            //return Json(iResultado, JsonRequestBehavior.AllowGet);
        }

        

    }
}