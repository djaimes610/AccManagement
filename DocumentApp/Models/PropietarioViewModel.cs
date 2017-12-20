using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentApp.Models
{
    public class PropietarioViewModel
    {
        public int id { get; set; }
        public int codigo { get; set; }
        public string descripcion { get; set; }
        public int estado { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}