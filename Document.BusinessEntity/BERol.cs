using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Propiedades de Roles generados.
    /// </summary>
    public class BERol : Auditoria
    {
        public int cod_rol { get; set; }
        public string gls_rol { get; set; }
    }
}
