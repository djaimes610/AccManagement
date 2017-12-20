using Document.BusinessEntity;
using Document.BusinessLogic;
using DocumentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentApp.Controllers
{
    public class Util
    {

        /// <summary>
        /// Obtiene los datos del Usuario en el ActiveDirectory
        /// </summary>
        /// <returns></returns>
        public static string GetUserAD()
        {
            return HttpContext.Current.Request.LogonUserIdentity.Name;
        }

        //Obtiene los datos del Usuario
        public static UsuarioLoginViewModel GetUser(string usuario)
        {
            UsuarioLoginViewModel UsuarioModel = new UsuarioLoginViewModel();
            BEUsuario oBEUsuario = new BEUsuario();
            BLUsuario oBLUsuario = new BLUsuario();

            oBEUsuario = oBLUsuario.ObtenerUsuarioLogin(usuario);

            if (oBEUsuario != null)
            {
                UsuarioModel.Codigo = oBEUsuario.cod_usuario;
                UsuarioModel.ApellidoMaterno = oBEUsuario.ape_materno;
                UsuarioModel.ApellidoPaterno = oBEUsuario.ape_paterno;
                UsuarioModel.Nombre = oBEUsuario.nombres;
                UsuarioModel.CodigoArea = oBEUsuario.cod_area;
                UsuarioModel.nombreArea = oBEUsuario.gls_area;
                UsuarioModel.idRol = oBEUsuario.cod_rol;
                UsuarioModel.nombreRol = oBEUsuario.gls_rol;
                UsuarioModel.Correo = oBEUsuario.correo;
                UsuarioModel.NombreCompleto = ConvertFirstLetterToUpper(UsuarioModel.Nombre + " " + UsuarioModel.ApellidoPaterno + " " + UsuarioModel.ApellidoMaterno);
                UsuarioModel.PrimerNombreApellido = ConvertFirstLetterToUpper(NombreApellidoPrimero(UsuarioModel.Nombre, UsuarioModel.ApellidoPaterno));
            }
            else
            {
                UsuarioModel = null;
            }

            return UsuarioModel;
        }


        public static string NombreApellidoPrimero(string Nombre, string ApellidoPaterno)
        {
            string PrimerNombre = Nombre.Split(' ').GetValue(0).ToString();
            string resultado = PrimerNombre + " " + ApellidoPaterno;
            return resultado;
        }

        public static string ConvertFirstLetterToUpper(string strWord)
        {
            try
            {
                string[] arrWords = strWord.Split(' ');
                string strTemp2 = string.Empty;

                if (arrWords.Length > 1)
                {
                    foreach (string strTemp in arrWords)
                    {
                        strTemp2 += strTemp.Substring(0, 1).ToUpper() + strTemp.Substring(1).ToLower() + " ";
                    }
                }
                else
                    strTemp2 = arrWords[0].Substring(0, 1).ToUpper() + arrWords[0].Substring(1).ToLower() + " ";

                return strTemp2.Substring(0, strTemp2.Length - 1);
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                return strWord;
            }
        }

        public static List<PropietarioViewModel> ListaPropietario()
        {
            List<PropietarioViewModel> Resultado = new List<PropietarioViewModel>();
            PropietarioViewModel propietario;
            int Numero = 0;

            BLPropietario PropietarioLogic = new BLPropietario();
            List<BEPropietario> ListaResultado = new List<BEPropietario>();
            ListaResultado = PropietarioLogic.ListarPropietario();

            foreach (BEPropietario item in ListaResultado)
            {
                Numero += 1;
                propietario = new PropietarioViewModel();
                propietario.id = Numero;
                propietario.codigo = item.cod_propietario;
                propietario.descripcion = item.gls_propietario;
                propietario.estado = item.cod_estado_registro;
                Resultado.Add(propietario);
            }

            return Resultado;
        }

    }
}