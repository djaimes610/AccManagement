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
    public class DAArea
    {
        public List<BEArea> ListarArea()
        {
            List<BEArea> oListado = new List<BEArea>();
            BEArea oItem;

            using (NpgsqlConnection ocn = new NpgsqlConnection(Util.getConnection()))
            {
                ocn.Open();
                NpgsqlTransaction tran = ocn.BeginTransaction();
                using (NpgsqlCommand ocmd = new NpgsqlCommand("public.func_listar_area", ocn))
                {
                    ocmd.CommandType = CommandType.StoredProcedure;

                    using (NpgsqlDataReader odr = ocmd.ExecuteReader())
                    {
                        while (odr.Read())
                        {
                            oItem = new BEArea();

                            if (!Convert.IsDBNull(odr["cod_area"]))
                                oItem.cod_area = Convert.ToInt32(odr["cod_area"]);

                            if (!Convert.IsDBNull(odr["gls_area"]))
                                oItem.gls_area = odr["gls_area"].ToString();

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
