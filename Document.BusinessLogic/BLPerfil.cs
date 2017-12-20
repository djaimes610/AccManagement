using Document.BusinessEntity;
using Document.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessLogic
{
    public class BLPerfil
    {
        public List<BEPerfil> ListarPerfiles()
        {
            return new DAPerfil().ListarPerfil();
        }

        public int Registrar(BEPerfil perfil)
        {
            return new DAPerfil().Registrar(perfil);
        }

        public int Actualizar(BEPerfil perfil)
        {
            return new DAPerfil().Actualizar(perfil);
        }

        /// <summary>
        /// Eliminar Propietario: eliminacion logica del registro propietario segun el codigo de registro enviado.
        /// </summary>
        /// <param name="perfil">parametros del propietario</param>
        /// <returns>valor de respuesta</returns>
        public int Eliminar(BEPerfil perfil)
        {
            return new DAPerfil().Eliminar(perfil);
        }

        public List<BEPerfil> GetPerfilesSinAsignar(string id)
        {
            return new DAPerfil().GetPerfilesSinAsignar(id);
        }

        public List<BEPerfil> GetPerfilesAsignados(string id)
        {
            return new DAPerfil().GetPerfilesAsignados(id);
        }

    }
}
