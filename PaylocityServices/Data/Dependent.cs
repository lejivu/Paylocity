using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityServices.Data
{
    public partial class Dependent
    {
        public int DependentID { get; set; }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
    }
}
