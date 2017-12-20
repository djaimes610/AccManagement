using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Document.BusinessEntity;
using Document.DataAccess;

namespace Document.BusinessLogic
{
    public class BLCarpeta
    {
        /// <summary>
        /// Registro de Carpetas
        /// </summary>
        /// <param name="oParametro">Parametros de Registro</param>
        /// <returns>Estado de la acción</returns>
        public int RegistrarCarpeta(BECarpeta oParametro)
        {
            return new DACarpeta().RegistrarCarpeta(oParametro);
        }

        /// <summary>
        /// ELiminar Carpeta: Solo se elimina si dicha carpeta no tiene documentos ni subcarpetas.
        /// (esto es una medida de seguridad para evitar eliminar documentos publicados)
        /// </summary>
        /// <param name="oParametro">Codigo de la Carpeta a eliminar</param>
        /// <returns>1 ELiminado Correctamente, -1 Debe eliminar Sub-Carpetas o Documentos existentes</returns>
        public int EliminarCarpeta(BECarpeta oParametro)
        {
            return new DACarpeta().EliminarCarpeta(oParametro);
        }

        /// <summary>
        /// Modifica los datos del registro carpeta
        /// </summary>
        /// <param name="oParametro">Codigo de carpeta a modificar</param>
        /// <returns>estado V/F</returns>
        public bool ModificarCarpeta(BECarpeta oParametro)
        {
            return new DACarpeta().ModificarCarpeta(oParametro);
        }

        public bool ModificarCarpetaGestion(BECarpeta oParametro)
        {
            return new DACarpeta().ModificarCarpetaGestion(oParametro);
        }

        public BECarpeta ObtenerCarpeta(int cod_carpeta)
        {
            return new DACarpeta().ObtenerCarpeta(cod_carpeta);
        }

        /// <summary>
        /// Listado de Carpeta
        /// </summary>
        /// <returns>Lista de registros</returns>
        public List<BECarpeta> Listar_Carpeta()
        {
            return new DACarpeta().ListarCarpeta();
        }
    }
}
