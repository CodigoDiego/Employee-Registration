using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EmployeeReg
    {
        [PrimaryKey, Identity]
        public int id { set; get; }
        public string nid { set; get; }
        public string name { set; get; }
        public string lname { set; get; }
        public string email { set; get; }
    }
}
