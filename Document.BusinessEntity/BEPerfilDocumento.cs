using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Asociacion Perfil Documento.
    /// </summary>
    public class BEPerfilDocumento : Auditoria
    {
        public int cod_perfil { get; set; }
        public int cod_documento { get; set; }
    }
}
