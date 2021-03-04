using Foundation.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity.Models
{
    public class EmployeeViewModel
    {
        public decimal PackageExpensePerPaycheck { get; set; }
        public decimal NetPayPerPaycheck { get; set; }
        public decimal TotalSalaryExpense { get; set; }
        public decimal TotalPaidPackageExpense { get; set; }
        public int NumberOfEmployees { get; set; }
        public List<Employee> employees { get; set; } = new List<Employee>();
        public Employee employee { get; set; }
    }
}
