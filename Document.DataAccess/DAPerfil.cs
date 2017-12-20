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
    public class DAPerfil
    {
        public List<BEPerfil> ListarPerfil()
        {
            List<BEPerfil> oListado = new List<BEPerfil>();
            BEPerfil oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_doc_perfiles", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BEPerfil();

                            if (!Convert.IsDBNull(odr["cod_perfil"]))
                                oItem.cod_perfil = Convert.ToInt32(odr["cod_perfil"]);

                            if (!Convert.IsDBNull(odr["gls_perfil"]))
                                oItem.gls_perfil = odr["gls_perfil"].ToString();

                            if (!Convert.IsDBNull(odr["cod_estado_registro"]))
                                oItem.cod_estado_registro = Convert.ToInt32(odr["cod_estado_registro"]);

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

        public int Registrar(BEPerfil perfil)
        {
            int idRegistro = 0;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    ocn.Open();
                    NpgsqlTransaction tran = ocn.BeginTransaction();
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_registrar_doc_perfil", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_gls_perfil", NpgsqlDbType.Varchar).Value = perfil.gls_perfil;
                        ocmd.Parameters.Add("@p_aud_usr_ingreso", NpgsqlDbType.Varchar).Value = perfil.aud_usr_ingreso;

                        using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                        {
                            while (odr.Read())
                                idRegistro = Convert.ToInt32(odr[0]);
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

        public int Actualizar(BEPerfil perfil)
        {
            int idRegistro = 0;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    ocn.Open();
                    NpgsqlTransaction tran = ocn.BeginTransaction();
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_actualizar_doc_perfil", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_perfil", NpgsqlDbType.Integer).Value = perfil.cod_perfil;
                        ocmd.Parameters.Add("@p_gls_perfil", NpgsqlDbType.Varchar).Value = perfil.gls_perfil;
                        ocmd.Parameters.Add("@p_aud_usr_modificacion", NpgsqlDbType.Varchar).Value = perfil.aud_usr_modificacion;

                        using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                        {
                            while (odr.Read())
                                idRegistro = Convert.ToInt32(odr[0]);
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
        /// Eliminar Propietario: eliminacion logica del registro propietario segun el codigo de registro enviado.
        /// </summary>
        /// <param name="propietario">parametros del propietario</param>
        /// <returns>valor de respuesta</returns>
        public int Eliminar(BEPerfil perfil)
        {
            int iResultado;

            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_delete_doc_perfil", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_perfil", NpgsqlDbType.Integer).Value = perfil.cod_perfil;
                        ocmd.Parameters.Add("@p_aud_usr_modificacion", NpgsqlDbType.Varchar).Value = perfil.aud_usr_modificacion;

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

        public List<BEPerfil> GetPerfilesSinAsignar(string id)
        {
            List<BEPerfil> oListado = new List<BEPerfil>();
            BEPerfil oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_perfiles_sin_asignar", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;
                    ocmd.Parameters.Add("@p_cod_usuario", NpgsqlDbType.Varchar).Value = id;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BEPerfil();

                            if (!Convert.IsDBNull(odr["cod_perfil"]))
                                oItem.cod_perfil = Convert.ToInt32(odr["cod_perfil"]);

                            if (!Convert.IsDBNull(odr["gls_perfil"]))
                                oItem.gls_perfil = odr["gls_perfil"].ToString();

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

        public List<BEPerfil> GetPerfilesAsignados(string id)
        {
            List<BEPerfil> oListado = new List<BEPerfil>();
            BEPerfil oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_perfiles_asignados", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;
                    ocmd.Parameters.Add("@p_cod_usuario", NpgsqlDbType.Varchar).Value = id;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BEPerfil();

                            if (!Convert.IsDBNull(odr["cod_perfil"]))
                                oItem.cod_perfil = Convert.ToInt32(odr["cod_perfil"]);

                            if (!Convert.IsDBNull(odr["gls_perfil"]))
                                oItem.gls_perfil = odr["gls_perfil"].ToString();

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
