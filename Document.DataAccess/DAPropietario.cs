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
    public class DAPropietario
    {
        public List<BEPropietario> ListarPropietario()
        {
            List<BEPropietario> oListado = new List<BEPropietario>();
            BEPropietario oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_doc_propietarios", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BEPropietario();

                            if (!Convert.IsDBNull(odr["cod_propietario"]))
                                oItem.cod_propietario = Convert.ToInt32(odr["cod_propietario"]);

                            if (!Convert.IsDBNull(odr["gls_propietario"]))
                                oItem.gls_propietario = odr["gls_propietario"].ToString();

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

        public int Registrar(BEPropietario propietario)
        {
            int idRegistro = 0;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    ocn.Open();
                    NpgsqlTransaction tran = ocn.BeginTransaction();
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_registrar_doc_propietario", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_gls_propietario", NpgsqlDbType.Varchar).Value = propietario.gls_propietario;
                        ocmd.Parameters.Add("@p_aud_usr_ingreso", NpgsqlDbType.Varchar).Value = propietario.aud_usr_ingreso;

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

        public int Actualizar(BEPropietario propietario)
        {
            int idRegistro = 0;
            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    ocn.Open();
                    NpgsqlTransaction tran = ocn.BeginTransaction();
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_actualizar_doc_propietario", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_propietario", NpgsqlDbType.Integer).Value = propietario.cod_propietario;
                        ocmd.Parameters.Add("@p_gls_propietario", NpgsqlDbType.Varchar).Value = propietario.gls_propietario;
                        ocmd.Parameters.Add("@p_aud_usr_modificacion", NpgsqlDbType.Varchar).Value = propietario.aud_usr_modificacion;

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
        public int Eliminar(BEPropietario propietario)
        {
            int iResultado;

            try
            {
                using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
                {
                    using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_delete_doc_propietario", ocn))
                    {
                        ocmd.CommandType = CommandType.StoredProcedure;
                        ocmd.Parameters.Add("@p_cod_propietario", NpgsqlDbType.Integer).Value = propietario.cod_propietario;
                        ocmd.Parameters.Add("@p_aud_usr_modificacion", NpgsqlDbType.Varchar).Value = propietario.aud_usr_modificacion;

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

    }
}
