using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class DbsConnect:LinqToDB.Data.DataConnection

    {
        //public DbsConnect() : base("") { }
        //DataConnection db = new DataConnection(@"Data Source=DSKTP-DGUV\DGUEV;Database=register_db;Integrated Security=True;");
        //DataConnection.DefaultSettings=new MySettings();
        //public DbsConnect() : base("dataBase") { }
        //public ITable<EmployeeReg> _employeereg { get{ return this.GetTable<EmployeeReg>(); } }
    }
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
            => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "dataBase",
                        ProviderName = ProviderName.SqlServer,
                        ConnectionString =
                            @"Data Source=DSKTP-DGUV\DGUEV;Database=employeesreg;Integrated Security=True;Encrypt=False;TrustServerCertificate = False;Trusted_Connection=True;Enlist=False;"
                        //Data Source = DSKTP - DGUV\DGUEV; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False       ,51158;User Id = admin; Password=adminpass
                    };
            }
        }
    }
}
