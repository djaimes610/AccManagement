using Document.BusinessEntity;
using Document.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessLogic
{
    public class BLPropietario
    {
        public List<BEPropietario> ListarPropietario()
        {
            return new DAPropietario().ListarPropietario();
        }

        public int Registrar(BEPropietario propietario)
        {
            return new DAPropietario().Registrar(propietario);
        }

        public int Actualizar(BEPropietario propietario)
        {
            return new DAPropietario().Actualizar(propietario);
        }

        /// <summary>
        /// Eliminar Propietario: eliminacion logica del registro propietario segun el codigo de registro enviado.
        /// </summary>
        /// <param name="propietario">parametros del propietario</param>
        /// <returns>valor de respuesta</returns>
        public int Eliminar(BEPropietario propietario)
        {
            return new DAPropietario().Eliminar(propietario);
        }

    }
}
