using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Propiedades de Tipos de documentos.
    /// </summary>
    public class BETipoDocumento : Auditoria
    {
        public int cod_tipo_documento { get; set; }
        public string gls_tipo_documento { get; set; }
    }
}
