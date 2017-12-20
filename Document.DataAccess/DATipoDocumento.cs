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
    public class DATipoDocumento
    {
        public List<BETipoDocumento> ListarTipoDocumento()
        {
            List<BETipoDocumento> oListado = new List<BETipoDocumento>();
            BETipoDocumento oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_doc_tipo_documento", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BETipoDocumento();

                            if (!Convert.IsDBNull(odr["cod_tipo_documento"]))
                                oItem.cod_tipo_documento = Convert.ToInt32(odr["cod_tipo_documento"]);

                            if (!Convert.IsDBNull(odr["gls_tipo_documento"]))
                                oItem.gls_tipo_documento = odr["gls_tipo_documento"].ToString();

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
    }
}
