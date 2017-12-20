using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Propiedad de Perfiles Generados.
    /// </summary>
    public class BEPerfil : Auditoria
    {
        public int cod_perfil { get; set; }
        public string gls_perfil { get; set; }

    }
}
