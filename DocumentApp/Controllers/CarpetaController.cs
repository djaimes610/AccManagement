using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentApp.Models;
using Document.BusinessEntity;
using Document.BusinessLogic;
using System.IO;

namespace DocumentApp.Controllers
{
    public class CarpetaController : Controller
    {
        // GET: Carpeta
        public ActionResult Gestionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Subir(HttpPostedFileBase archivo, string tdescripcion, string tcodtipodoc,string tcodpropietary, string tfvigenci, string tidcarpeta)
        {
            BLDocumento oLogicDoc = new BLDocumento();
            BEDocumento odocument = new BEDocumento();

            //registro de la informacion
            string nombreArchivo = archivo.FileName.ObtenerMD5() + Path.GetExtension(archivo.FileName);

            odocument.gls_nombre_documento = tdescripcion;
            odocument.gls_nombre_archivo = nombreArchivo;
            odocument.cod_tipo_documento = Convert.ToInt16(tcodtipodoc);
            odocument.cod_propietario = Convert.ToInt16(tcodpropietary);
            odocument.cod_carpeta = Convert.ToInt16(tidcarpeta);
            odocument.fec_vigencia = Convert.ToDateTime(tfvigenci);
            odocument.aud_usr_ingreso = ".NET";// UsuarioActual;

            int idCodigo = oLogicDoc.RegistrarDocumento(odocument);

            if (idCodigo > 0)
            {
                //Si el documento se registro correctamente en la BD, se procede con la Carga del Archivo al Servidor
                archivo.SaveAs(Path.Combine(Server.MapPath("~/Archivos"), nombreArchivo));
            }

            return RedirectToAction("Gestionar", "Carpeta");
            //return Content(Convert.ToString(idCodigo));
        }

        public FileResult Download(string code)
        {
            BEDocumento odownload = new BEDocumento();
            BLDocumento oldownload = new BLDocumento();

            odownload = oldownload.ObtenerDocumentoDownload(Convert.ToInt16(code));
            string miruta = Server.MapPath("~") + "\\Archivos\\";

            string getExtension = Path.GetExtension(miruta + odownload.gls_nombre_archivo);

            byte[] fileBytes = System.IO.File.ReadAllBytes(miruta + odownload.gls_nombre_archivo);
            string fileName = odownload.gls_nombre_documento + getExtension;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public JsonResult CreateNewFolder(string idFolderPadre, string nameFolder)
        {
            BLCarpeta lgFolder = new BLCarpeta();
            BECarpeta newFolder = new BECarpeta();
            newFolder.cod_carpeta_padre = Convert.ToInt16(idFolderPadre);
            newFolder.gls_ruta_carpeta = nameFolder;
            newFolder.aud_usr_ingreso = ".NET";
            int idFolder = lgFolder.RegistrarCarpeta(newFolder);
            return Json(idFolder, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTreeviewFiles()
        {
            List<BECarpeta> oListaCarpeta;
            List<BECarpeta> oListaCarpetaMenus = new List<BECarpeta>();
            List<CarpetaViewModel> _ListCarpeta = new List<CarpetaViewModel>();

            //Obtenemos el listado
            oListaCarpeta = new BLCarpeta().Listar_Carpeta();

            if (oListaCarpeta.Count > 0)
            {
                oListaCarpetaMenus = oListaCarpeta.Where(e => e.cod_carpeta_padre < 1).ToList();

                CarpetaViewModel _carpeta;

                foreach (BECarpeta item in oListaCarpetaMenus)
                {
                    _carpeta = new CarpetaViewModel();
                    _carpeta.id = item.cod_carpeta;
                    _carpeta.text = " " + item.gls_ruta_carpeta;
                    _carpeta.nodes = GetAllNodosHijos(oListaCarpeta, item.cod_carpeta);
                    _ListCarpeta.Add(_carpeta);
                }
            }
            
            return Json(_ListCarpeta, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene todos los nodos referentes al nodo padre solicitado
        /// </summary>
        /// <param name="listado">Lista de nodos existentes</param>
        /// <param name="">id nodo padre para devolver los que le pertenecen</param>
        /// <returns>Lista de nodos hijos</returns>
        private List<CarpetaViewModel> GetAllNodosHijos(List<BECarpeta> listado, int nodoPadre)
        {
            CarpetaViewModel nodoHijo;
            List<CarpetaViewModel> NodosHijos = new List<CarpetaViewModel>();

            foreach (BECarpeta item in listado)
            {
                if (item.cod_carpeta_padre == nodoPadre)
                {
                    nodoHijo = new CarpetaViewModel();
                    nodoHijo.id = item.cod_carpeta;
                    nodoHijo.text = " " + item.gls_ruta_carpeta;
                    List<CarpetaViewModel> nhijos;

                    nhijos = GetAllNodosHijos(listado, item.cod_carpeta);
                    if (nhijos.Count > 0)
                        nodoHijo.nodes = nhijos;
                    else
                        nodoHijo.nodes = null;

                    NodosHijos.Add(nodoHijo);
                }
            }

            return NodosHijos;
        }

        public JsonResult GetAllDocumentosPorCarpeta(string id)
        {
            List<DocumentoViewModel> Documentos = new List<DocumentoViewModel>();
            DocumentoViewModel documento;

            List<BEDocumento> olistaDocumentos;
            BLDocumento oBldocumento = new BLDocumento();

            int nroCarpeta = Convert.ToInt16(id);

            olistaDocumentos = oBldocumento.ListarDocumento_porCarpeta(nroCarpeta);

            int iCorrelativo = 0;

            foreach (BEDocumento item in olistaDocumentos)
            {
                documento = new DocumentoViewModel();
                iCorrelativo += 1;

                documento.nro = iCorrelativo;
                documento.id = item.cod_documento;
                documento.documento = item.gls_nombre_documento;
                documento.tipoDocumento = item.gls_tipo_documento;
                documento.propietario = item.gls_propietario;
                documento.fechaVigencia = item.fec_vigencia.ToString("dd-MM-yyyy");
                Documentos.Add(documento);
            }

            return Json(Documentos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllTipoDocumentos()
        {
            List<TipoDocumentoViewModel> Lista = new List<TipoDocumentoViewModel>();
            TipoDocumentoViewModel tipoDocumento;

            List<BETipoDocumento> oListaTipoDocumento;
            BLTipoDocumento oBLTipoDocumento = new BLTipoDocumento();
            oListaTipoDocumento = oBLTipoDocumento.ListarTipoDocumento();

            foreach (BETipoDocumento item in oListaTipoDocumento)
            {
                tipoDocumento = new TipoDocumentoViewModel();
                tipoDocumento.codigo = item.cod_tipo_documento;
                tipoDocumento.descripcion = item.gls_tipo_documento;
                tipoDocumento.estado = item.cod_estado_registro;
                Lista.Add(tipoDocumento);
            }

            return Json(Lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPropietario()
        {
            List<PropietarioViewModel> Lista = new List<PropietarioViewModel>();
            PropietarioViewModel propietario;

            List<BEPropietario> oListaPropietario;
            BLPropietario oBLPropietario = new BLPropietario();
            oListaPropietario = oBLPropietario.ListarPropietario();

            foreach (BEPropietario item in oListaPropietario)
            {
                propietario = new PropietarioViewModel();
                propietario.codigo = item.cod_propietario;
                propietario.descripcion = item.gls_propietario;
                propietario.estado = item.cod_estado_registro;
                Lista.Add(propietario);
            }

            return Json(Lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult deleteFolder(string id)
        {
            int iResult;

            BLCarpeta oBLCarpeta = new BLCarpeta();
            BECarpeta oitem = new BECarpeta();

            oitem.cod_carpeta = Convert.ToInt16(id);
            oitem.aud_usr_modificacion = "APP.DLT";

            iResult = oBLCarpeta.EliminarCarpeta(oitem);

            return Json(iResult,JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateNameFolder(string id, string desc)
        {
            bool estadoUpdate = false;
            BLCarpeta oBLCarpeta = new BLCarpeta();
            BECarpeta oitem = new BECarpeta();

            oitem.cod_carpeta = Convert.ToInt16(id);
            oitem.gls_ruta_carpeta = desc;
            oitem.cod_estado_registro = 1;
            oitem.aud_usr_modificacion = "APP.NET";

            estadoUpdate = oBLCarpeta.ModificarCarpetaGestion(oitem);

            return Json(estadoUpdate, JsonRequestBehavior.AllowGet);
        }

    }
}