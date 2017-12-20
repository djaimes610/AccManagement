using Document.BusinessEntity;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.DataAccess
{
    public class DADocumento
    {
        /// <summary>
        /// Registro de Documento
        /// </summary>
        /// <param name="oParametro">Detalle del documento</param>
        /// <returns>Codigo generado</returns>
        public int RegistrarDocumento(BEDocumento oParametro)
        {
            int idRegistro = 0;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    ocn.Open();
                    NpgsqlTransaction tran = ocn.BeginTransaction();
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_registrar_doc_documentos", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_gls_nombre_documento", NpgsqlDbType.Varchar).Value = oParametro.gls_nombre_documento;
                        ocmd.Parameters.Add("@p_gls_nombre_archivo", NpgsqlDbType.Varchar).Value = oParametro.gls_nombre_archivo;
                        ocmd.Parameters.Add("@p_cod_tipo_documento", NpgsqlDbType.Integer).Value = oParametro.cod_tipo_documento;
                        ocmd.Parameters.Add("@p_cod_propietario", NpgsqlDbType.Integer).Value = oParametro.cod_propietario;
                        ocmd.Parameters.Add("@p_fec_vigencia", NpgsqlDbType.Date).Value = oParametro.fec_vigencia;
                        ocmd.Parameters.Add("@p_cod_carpeta", NpgsqlDbType.Integer).Value = oParametro.cod_carpeta;
                        ocmd.Parameters.Add("@p_aud_usr_ingreso", NpgsqlDbType.Varchar).Value = oParametro.aud_usr_ingreso;

                        using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                        {
                            while (odr.Read())
                            {
                                idRegistro = Convert.ToInt32(odr[0]);
                            }
                        }

                    }
                    tran.Commit();
                    ocn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idRegistro;
        }

        /// <summary>
        /// Modificacion del documento
        /// </summary>
        /// <param name="oParametro">Detalle de la modificacion</param>
        /// <returns>Estado de actualización V/F</returns>
        public bool ModificarDocumento(BEDocumento oParametro)
        {
            bool estadoIngreso = false;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_modificar_doc_documento", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_documento", NpgsqlDbType.Integer).Value = oParametro.cod_documento;
                        ocmd.Parameters.Add("@p_gls_nombre_documento", NpgsqlDbType.Varchar).Value = oParametro.gls_nombre_documento;
                        ocmd.Parameters.Add("@p_cod_tipo_documento", NpgsqlDbType.Integer).Value = oParametro.cod_tipo_documento;
                        ocmd.Parameters.Add("@p_cod_propietario_documento", NpgsqlDbType.Integer).Value = oParametro.cod_propietario;
                        ocmd.Parameters.Add("@p_fec_vigencia", NpgsqlDbType.Date).Value = oParametro.fec_vigencia;
                        //ocmd.Parameters.Add("@p_gls_ruta_documento", NpgsqlDbType.Varchar).Value = oParametro.gls_ruta_documento;
                        ocmd.Parameters.Add("@p_aud_usr_modificacion", NpgsqlDbType.Varchar).Value = oParametro.aud_usr_modificacion;

                        ocn.Open();
                        ocmd.ExecuteNonQuery();
                        estadoIngreso = true;
                    }
                    ocn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoIngreso;
        }

        /// <summary>
        /// Obtener documento especifico
        /// </summary>
        /// <param name="cod_documento">Codigo del documento</param>
        /// <returns>Detalle del documento</returns>
        public BEDocumento ObtenerDocumento(int cod_documento)
        {
            BEDocumento oItemResult = new BEDocumento();

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_obtener_doc_documento", ocn))
                {
                    ocmd.Parameters.Add("@p_cod_documento", NpgsqlDbType.Integer).Value = cod_documento;
                    ocmd.CommandType = CommandType.StoredProcedure;
                    ocmd.ExecuteNonQuery();

                    ocmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
                    ocmd.CommandType = CommandType.Text;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {

                            if (!Convert.IsDBNull(odr["cod_documento"]))
                                oItemResult.cod_documento = Convert.ToInt32(odr["cod_documento"]);

                            if (!Convert.IsDBNull(odr["gls_nombre_documento"]))
                                oItemResult.gls_nombre_documento = odr["gls_nombre_documento"].ToString();

                            if (!Convert.IsDBNull(odr["cod_tipo_documento"]))
                                oItemResult.cod_tipo_documento = Convert.ToInt32(odr["cod_tipo_documento"]);

                            //if (!Convert.IsDBNull(odr["cod_propietario_documento"]))
                                //oItemResult.cod_propietario = Convert.ToInt32(odr["cod_propietario_documento"]);

                            if (!Convert.IsDBNull(odr["fec_vigencia"]))
                                oItemResult.fec_vigencia = Convert.ToDateTime(odr["fec_vigencia"]);

                            //if (!Convert.IsDBNull(odr["gls_ruta_documento"]))
                                //oItemResult.gls_ruta_documento = odr["gls_ruta_documento"].ToString();

                            if (!Convert.IsDBNull(odr["aud_fec_ingreso"]))
                                oItemResult.aud_fec_ingreso = Convert.ToDateTime(odr["aud_fec_ingreso"]);

                            if (!Convert.IsDBNull(odr["aud_usr_modificacion"]))
                                oItemResult.aud_usr_modificacion = odr["aud_usr_modificacion"].ToString();

                            if (!Convert.IsDBNull(odr["aud_fec_modificacion"]))
                                oItemResult.aud_fec_modificacion = Convert.ToDateTime(odr["aud_fec_modificacion"]);

                        }
                        odr.Close();
                    }
                }
                tran.Commit();
                ocn.Close();
            }

            return oItemResult;
        }

        public BEDocumento ObtenerDocumentoDownload(int cod_documento)
        {
            BEDocumento oItemResult = new BEDocumento();

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_obtener_doc_documento_download", ocn))
                {
                    ocmd.Parameters.Add("@p_cod_documento", NpgsqlDbType.Integer).Value = cod_documento;
                    ocmd.CommandType = CommandType.StoredProcedure;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {

                            if (!Convert.IsDBNull(odr["cod_documento"]))
                                oItemResult.cod_documento = Convert.ToInt32(odr["cod_documento"]);

                            if (!Convert.IsDBNull(odr["gls_nombre_documento"]))
                                oItemResult.gls_nombre_documento = odr["gls_nombre_documento"].ToString();

                            if (!Convert.IsDBNull(odr["gls_nombre_archivo"]))
                                oItemResult.gls_nombre_archivo = odr["gls_nombre_archivo"].ToString();

                        }
                        odr.Close();
                    }
                }
                tran.Commit();
                ocn.Close();
            }

            return oItemResult;
        }

        /// <summary>
        /// Listado de documentos
        /// </summary>
        /// <returns>Lista de documentos encontrados</returns>
        public List<BEDocumento> ListarDocumento()
        {
            List<BEDocumento> oListado = new List<BEDocumento>();
            BEDocumento oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_doc_documento", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;
                    ocmd.ExecuteNonQuery();

                    ocmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
                    ocmd.CommandType = CommandType.Text;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BEDocumento();

                            if (!Convert.IsDBNull(odr["cod_documento"]))
                                oItem.cod_documento = Convert.ToInt32(odr["cod_documento"]);

                            if (!Convert.IsDBNull(odr["gls_nombre_documento"]))
                                oItem.gls_nombre_documento = odr["gls_nombre_documento"].ToString();

                            if (!Convert.IsDBNull(odr["gls_nombre_archivo"]))
                                oItem.gls_nombre_archivo = odr["gls_nombre_archivo"].ToString();

                            if (!Convert.IsDBNull(odr["cod_tipo_documento"]))
                                oItem.cod_tipo_documento = Convert.ToInt32(odr["cod_tipo_documento"]);

                            //if (!Convert.IsDBNull(odr["cod_propietario"]))
                            //    oItem.cod_propietario = Convert.ToInt32(odr["cod_propietario"]);

                            if (!Convert.IsDBNull(odr["fec_vigencia"]))
                                oItem.fec_vigencia = Convert.ToDateTime(odr["fec_vigencia"]);

                            if (!Convert.IsDBNull(odr["aud_fec_ingreso"]))
                                oItem.aud_fec_ingreso = Convert.ToDateTime(odr["aud_fec_ingreso"]);

                            if (!Convert.IsDBNull(odr["aud_usr_modificacion"]))
                                oItem.aud_usr_modificacion = odr["aud_usr_modificacion"].ToString();

                            if (!Convert.IsDBNull(odr["aud_fec_modificacion"]))
                                oItem.aud_fec_modificacion = Convert.ToDateTime(odr["aud_fec_modificacion"]);

                            oListado.Add(oItem);
                        }
                        odr.Close();
                    }
                }
                tran.Commit();
                ocn.Close();
            }

            return oListado;
        }

        /// <summary>
        /// Listar documento por carpeta
        /// </summary>
        /// <param name="cod_carpeta">codigo de carpeta</param>
        /// <returns></returns>
        public List<BEDocumento> ListarDocumento_porCarpeta(int cod_carpeta)
        {
            List<BEDocumento> oListado = new List<BEDocumento>();
            BEDocumento oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_doc_documentos_por_carpeta", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;
                    ocmd.Parameters.Add("@pcodcarpeta", NpgsqlDbType.Integer).Value = cod_carpeta;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BEDocumento();

                            if (!Convert.IsDBNull(odr["cod_documento"]))
                                oItem.cod_documento = Convert.ToInt32(odr["cod_documento"]);

                            if (!Convert.IsDBNull(odr["gls_nombre_documento"]))
                                oItem.gls_nombre_documento = odr["gls_nombre_documento"].ToString();

                            if (!Convert.IsDBNull(odr["gls_nombre_archivo"]))
                                oItem.gls_nombre_archivo = odr["gls_nombre_archivo"].ToString();

                            if (!Convert.IsDBNull(odr["gls_tipo_documento"]))
                                oItem.gls_tipo_documento = odr["gls_tipo_documento"].ToString();

                            if (!Convert.IsDBNull(odr["gls_propietario"]))
                                oItem.gls_propietario = odr["gls_propietario"].ToString();

                            if (!Convert.IsDBNull(odr["fec_vigencia"]))
                                oItem.fec_vigencia = Convert.ToDateTime(odr["fec_vigencia"]);

                            oListado.Add(oItem);
                        }
                        odr.Close();
                    }
                }
                tran.Commit();
                ocn.Close();
            }

            return oListado;
        }

    }
}
