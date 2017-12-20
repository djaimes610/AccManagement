using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Document.DataAccess
{
    public static class Util
    {
        public static string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static string getConnecionMySql()
        {
            return ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        }
    }
}
