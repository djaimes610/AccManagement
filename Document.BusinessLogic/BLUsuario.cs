using Document.BusinessEntity;
using Document.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessLogic
{
    public class BLUsuario
    {
        /// <summary>
        /// Funcion: ObtenerUsuarioLogin
        /// Obtiene los datos del usuario que se valida.
        /// </summary>
        /// <param name="usuario">Usuario a Validar</param>
        /// <returns>Detalle del Usuario</returns>
        public BEUsuario ObtenerUsuarioLogin(string usuario)
        {
            return new DAUsuario().ObtenerUsuarioLogin(usuario);
        }

        /// <summary>
        /// Registro de Documento
        /// </summary>
        /// <returns>Codigo generado</returns>
        public List<BEUsuario> ListarUsuarios()
        {
            return new DAUsuario().ListarUsuarios();
        }

        public int Eliminar(BEUsuario usuario)
        {
            return new DAUsuario().Eliminar(usuario);
        }

        public int Registrar(BEUsuario usuario)
        {
            return new DAUsuario().Registrar(usuario);
        }

        public int Actualizar(BEUsuario usuario)
        {
            return new DAUsuario().Actualizar(usuario);
        }


    }
}
