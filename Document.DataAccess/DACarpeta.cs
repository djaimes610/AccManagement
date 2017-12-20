using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Document.BusinessEntity;
using Npgsql;
using NpgsqlTypes;

namespace Document.DataAccess
{
    public class DACarpeta
    {
        /// <summary>
        /// Registro de Carpetas
        /// </summary>
        /// <param name="oParametro">Parametros de Registro</param>
        /// <returns>Estado de la acción</returns>
        public int RegistrarCarpeta(BECarpeta oParametro)
        {
            int idRegistro = 0;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    ocn.Open();
                    NpgsqlTransaction tran = ocn.BeginTransaction();
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_registrar_doc_carpetas", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_gls_ruta_carpeta", NpgsqlDbType.Varchar).Value = oParametro.gls_ruta_carpeta;
                        ocmd.Parameters.Add("@p_cod_carpeta_padre", NpgsqlDbType.Integer).Value = oParametro.cod_carpeta_padre;
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
                //estadoIngreso = false;
            }
            return idRegistro;
        }

        /// <summary>
        /// ELiminar Carpeta: Solo se elimina si dicha carpeta no tiene documentos ni subcarpetas.
        /// (esto es una medida de seguridad para evitar eliminar documentos publicados)
        /// </summary>
        /// <param name="oParametro">Codigo de la Carpeta a eliminar</param>
        /// <returns>1 ELiminado Correctamente, -1 Debe eliminar Sub-Carpetas o Documentos existentes</returns>
        public int EliminarCarpeta(BECarpeta oParametro)
        {
            int iResultado;

            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_delete_doc_carpeta", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_carpeta", NpgsqlDbType.Integer).Value = oParametro.cod_carpeta;
                        ocmd.Parameters.Add("@p_aud_usr_modificacion", NpgsqlDbType.Varchar).Value = oParametro.aud_usr_modificacion;

                        ocn.Open();
                        iResultado = ocmd.ExecuteNonQuery();
                    }
                    ocn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //estadoIngreso = false;
            }

            return iResultado;
        }

        /// <summary>
        /// Modifica los datos del registro carpeta
        /// </summary>
        /// <param name="oParametro">Codigo de carpeta a modificar</param>
        /// <returns>estado V/F</returns>
        public bool ModificarCarpeta(BECarpeta oParametro)
        {
            bool estadoIngreso = false;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_modificar_doc_carpeta", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_carpeta", NpgsqlDbType.Integer).Value = oParametro.cod_carpeta;
                        ocmd.Parameters.Add("@p_gls_ruta_carpeta", NpgsqlDbType.Varchar).Value = oParametro.gls_ruta_carpeta;
                        ocmd.Parameters.Add("@p_cod_estado_registro", NpgsqlDbType.Integer).Value = oParametro.cod_estado_registro;
                        ocmd.Parameters.Add("@p_cod_carpeta_padre", NpgsqlDbType.Integer).Value = oParametro.cod_carpeta_padre;

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
                //estadoIngreso = false;
            }
            return estadoIngreso;
        }

        public bool ModificarCarpetaGestion(BECarpeta oParametro)
        {
            bool estadoIngreso = false;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_modificar_doc_carpeta_desc", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_carpeta", NpgsqlDbType.Integer).Value = oParametro.cod_carpeta;
                        ocmd.Parameters.Add("@p_gls_ruta_carpeta", NpgsqlDbType.Varchar).Value = oParametro.gls_ruta_carpeta;
                        ocmd.Parameters.Add("@p_cod_estado_registro", NpgsqlDbType.Integer).Value = oParametro.cod_estado_registro;
                        ocmd.Parameters.Add("@p_aud_usr_modificacion", NpgsqlDbType.Varchar).Value = oParametro.aud_usr_modificacion;

                        ocn.Open();
                        ocmd.ExecuteNonQuery();
                        estadoIngreso = true;
                    }
                    ocn.Close();
                }
            }
            catch (Exception)
            {
                //throw ex;
                estadoIngreso = false;
            }
            return estadoIngreso;
        }

        /// <summary>
        /// Obtener detaller de Carpeta
        /// </summary>
        /// <param name="cod_carpeta">Codigo de Carpeta</param>
        /// <returns>Detalles de Carpeta</returns>
        public BECarpeta ObtenerCarpeta(int cod_carpeta)
        {
            BECarpeta oItemResult = new BECarpeta();

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_obtener_doc_carpetas", ocn))
                {
                    ocmd.Parameters.Add("@p_cod_carpeta", NpgsqlDbType.Integer).Value = cod_carpeta;
                    ocmd.CommandType = CommandType.StoredProcedure;
                    ocmd.ExecuteNonQuery();

                    ocmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
                    ocmd.CommandType = CommandType.Text;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            
                            if (!Convert.IsDBNull(odr["cod_carpeta"]))
                                oItemResult.cod_carpeta = Convert.ToInt32(odr["cod_carpeta"]);

                            if (!Convert.IsDBNull(odr["gls_ruta_carpeta"]))
                                oItemResult.gls_ruta_carpeta = odr["gls_ruta_carpeta"].ToString();

                            if (!Convert.IsDBNull(odr["cod_carpeta_padre"]))
                                oItemResult.cod_carpeta_padre = Convert.ToInt32(odr["cod_carpeta_padre"]);

                            if (!Convert.IsDBNull(odr["cod_estado_registro"]))
                                oItemResult.cod_estado_registro = Convert.ToInt32(odr["cod_estado_registro"]);

                            if (!Convert.IsDBNull(odr["aud_usr_ingreso"]))
                                oItemResult.aud_usr_ingreso = odr["aud_usr_ingreso"].ToString();

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


        /// <summary>
        /// Listado de Carpeta
        /// </summary>
        /// <returns>Lista de registros</returns>
        public List<BECarpeta> ListarCarpeta()
        {
            List<BECarpeta> oListado = new List<BECarpeta>();
            BECarpeta oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_doc_carpetas", ocn))
                {
                    ocmd.Parameters.Add("@ref",NpgsqlDbType.Refcursor).Value= "carpet_cur";
                    ocmd.CommandType = CommandType.StoredProcedure;
                    ocmd.ExecuteNonQuery();

                    ocmd.CommandText = "fetch all in \"carpet_cur\"";
                    ocmd.CommandType = CommandType.Text;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BECarpeta();

                            if (!Convert.IsDBNull(odr["cod_carpeta"]))
                                oItem.cod_carpeta = Convert.ToInt32(odr["cod_carpeta"]);

                            if (!Convert.IsDBNull(odr["gls_ruta_carpeta"]))
                                oItem.gls_ruta_carpeta = odr["gls_ruta_carpeta"].ToString();

                            if (!Convert.IsDBNull(odr["cod_carpeta_padre"]))
                                oItem.cod_carpeta_padre = Convert.ToInt32(odr["cod_carpeta_padre"]);

                            if (!Convert.IsDBNull(odr["cod_estado_registro"]))
                                oItem.cod_estado_registro = Convert.ToInt32(odr["cod_estado_registro"]);

                            if (!Convert.IsDBNull(odr["aud_usr_ingreso"]))
                                oItem.aud_usr_ingreso = odr["aud_usr_ingreso"].ToString();

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

        ///// <summary>
        ///// Listado de Carpeta
        ///// </summary>
        ///// <returns>Lista de registros</returns>
        //public List<BECarpeta> ListarCarpeta_old()
        //{
        //    List<BECarpeta> oListado = new List<BECarpeta>();
        //    BECarpeta oItem;

        //    using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
        //    {
        //        ocn.Open();
        //        NpgsqlTransaction tran = ocn.BeginTransaction();
        //        using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_doc_carpetas", ocn))
        //        {
        //            ocmd.CommandType = CommandType.StoredProcedure;
        //            ocmd.ExecuteNonQuery();

        //            ocmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
        //            ocmd.CommandType = CommandType.Text;

        //            using (NpgsqlDataReader odr = ocmd.ExecuteReader())
        //            {
        //                while (odr.Read())
        //                {
        //                    oItem = new BECarpeta();

        //                    if (!Convert.IsDBNull(odr["cod_carpeta"]))
        //                        oItem.cod_carpeta = Convert.ToInt32(odr["cod_carpeta"]);

        //                    if (!Convert.IsDBNull(odr["gls_ruta_carpeta"]))
        //                        oItem.gls_ruta_carpeta = odr["gls_ruta_carpeta"].ToString();

        //                    if (!Convert.IsDBNull(odr["cod_carpeta_padre"]))
        //                        oItem.cod_carpeta_padre = Convert.ToInt32(odr["cod_carpeta_padre"]);

        //                    if (!Convert.IsDBNull(odr["cod_estado_registro"]))
        //                        oItem.cod_estado_registro = Convert.ToInt32(odr["cod_estado_registro"]);

        //                    if (!Convert.IsDBNull(odr["aud_usr_ingreso"]))
        //                        oItem.aud_usr_ingreso = odr["aud_usr_ingreso"].ToString();

        //                    if (!Convert.IsDBNull(odr["aud_fec_ingreso"]))
        //                        oItem.aud_fec_ingreso = Convert.ToDateTime(odr["aud_fec_ingreso"]);

        //                    if (!Convert.IsDBNull(odr["aud_usr_modificacion"]))
        //                        oItem.aud_usr_modificacion = odr["aud_usr_modificacion"].ToString();

        //                    if (!Convert.IsDBNull(odr["aud_fec_modificacion"]))
        //                        oItem.aud_fec_modificacion = Convert.ToDateTime(odr["aud_fec_modificacion"]);

        //                    oListado.Add(oItem);
        //                }
        //                odr.Close();
        //            }
        //        }
        //        tran.Commit();
        //        ocn.Close();
        //    }

        //    return oListado;
        //}

    }
}
