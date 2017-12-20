using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Propiedades de Areas
    /// </summary>
    public class BEArea : Auditoria
    {
        public int cod_area { get; set; }
        public string gls_area { get; set; }

    }
}
