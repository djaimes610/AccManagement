using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentApp.Models
{
    public class PerfilViewModel
    {
    public IEnumerable<Perfil> Items { get; set; }
    public Pager Pager { get; set; }
    }

    public class Perfil
    {
        public int Nro { get; set; }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}