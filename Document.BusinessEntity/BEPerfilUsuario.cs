using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessEntity
{
    /// <summary>
    /// Asociacion Perfil Documento
    /// </summary>
    public class BEPerfilUsuario : Auditoria
    {
        public int cod_usuario { get; set; }
        public int cod_perfil { get; set; }
    }
}
