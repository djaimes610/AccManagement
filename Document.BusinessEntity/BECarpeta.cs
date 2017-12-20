using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Propiedad de carpetas creadas para almacenar los documentos
    /// </summary>
    public class BECarpeta : Auditoria
    {
        public int cod_carpeta { get; set; }
        public int cod_carpeta_padre { get; set; }
        public string gls_ruta_carpeta { get; set; }

    }
}
