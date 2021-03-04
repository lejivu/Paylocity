using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityServices.Data
{
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal AnnualPackageExpense { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool isEmployerPackagePaid { get; set; }
        public decimal NetPay { get; set; }
        public virtual ICollection<Dependent> Dependents { get; set; }
        public bool isDeleted { get; set; }
    }
}
