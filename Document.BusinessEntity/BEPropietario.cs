using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Propiedades de Propietario.
    /// </summary>
    public class BEPropietario : Auditoria
    {
        public int cod_propietario { get; set; }
        public string gls_propietario { get; set; }
    }
}
