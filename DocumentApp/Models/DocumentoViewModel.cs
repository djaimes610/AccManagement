using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentApp.Models
{
    public class DocumentoViewModel
    {
        public int nro { get; set; }
        public int id { get; set; }
        public string documento { get; set; }
        public string tipoDocumento { get; set; }
        public string propietario { get; set; }
        public string fechaVigencia { get; set; }

    }
}