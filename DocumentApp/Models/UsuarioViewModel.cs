using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentApp.Models
{
    public class UsuarioViewModel
    {

        public IEnumerable<Usuario> Items { get; set; }
        public Pager Pager { get; set; }


    } 

    public class Usuario
    {
        public int Nro { get; set; }
        public string CodigoUsuario { get; set; }
        public int CodigoArea { get; set; }
        public int CodigoRol { get; set; }
        public string Area { get; set; }
        public string Rol { get; set; }
        public int Estado { get; set; }
        public string UsuarioIngreso { get; set; }
        public string FechaIngreso { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }

        public string NombreCompleto { get { return string.Format("{0} {1}, {2}", ApellidoPaterno, ApellidoMaterno, Nombres); } }
    }

} 