using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data
{
    public class Dconnect
    {
        public SqlConnection GetConnection() 
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=DSKTP-DGUV\DGUEV;Database=employeesreg;Integrated Security=True;Encrypt=False;TrustServerCertificate = False;Trusted_Connection=True;Enlist=False;";
            return con;
        }
        
    }
}
