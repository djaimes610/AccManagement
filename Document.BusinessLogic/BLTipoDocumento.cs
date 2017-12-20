using Document.BusinessEntity;
using Document.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessLogic
{
    public class BLTipoDocumento
    {
        public List<BETipoDocumento> ListarTipoDocumento()
        {
            return new DATipoDocumento().ListarTipoDocumento();
        }
    }
}
