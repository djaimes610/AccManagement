using Document.BusinessEntity;
using Document.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.BusinessLogic
{
    public class BLDocumento
    {
        /// <summary>
        /// Registro de Documento
        /// </summary>
        /// <param name="oParametro">Detalle del documento</param>
        /// <returns>Codigo generado</returns>
        public int RegistrarDocumento(BEDocumento oParametro)
        {
            return new DADocumento().RegistrarDocumento(oParametro);
        }

        /// <summary>
        /// Modificacion del documento
        /// </summary>
        /// <param name="oParametro">Detalle de la modificacion</param>
        /// <returns>Estado de actualización V/F</returns>
        public bool ModificarDocumento(BEDocumento oParametro)
        {
            return new DADocumento().ModificarDocumento(oParametro);
        }

        /// <summary>
        /// Obtener documento especifico
        /// </summary>
        /// <param name="cod_documento">Codigo del documento</param>
        /// <returns>Detalle del documento</returns>
        public BEDocumento ObtenerDocumento(int cod_documento)
        {
            return new DADocumento().ObtenerDocumento(cod_documento);
        }

        public BEDocumento ObtenerDocumentoDownload(int cod_documento)
        {
            return new DADocumento().ObtenerDocumentoDownload(cod_documento);
        }

        /// <summary>
        /// Listado de documentos
        /// </summary>
        /// <returns>Lista de documentos encontrados</returns>
        public List<BEDocumento> ListarDocumento()
        {
            return new DADocumento().ListarDocumento();
        }

        public List<BEDocumento> ListarDocumento_porCarpeta(int cod_carpeta)
        {
            return new DADocumento().ListarDocumento_porCarpeta(cod_carpeta);
        }

    }
}
