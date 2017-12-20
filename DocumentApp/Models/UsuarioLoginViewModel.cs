using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentApp.Models
{
    public class UsuarioLoginViewModel
    {
        public string Codigo { get; set; }
        public int CodigoArea { get; set; }
        public string nombreArea { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string NombreCompleto { get; set; }
        public string PrimerNombreApellido { get; set; }

        //ROL
        public int idRol { get; set; }
        public string nombreRol { get; set; }
    }
}