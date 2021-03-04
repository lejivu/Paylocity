using System;

namespace PaylocityServices.Data
{
    public partial class Paycheck
    {
        public int PaycheckID { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal TotalTaxes { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? DepositDate { get; set; }
        public bool isOutstanding { get; set; }
        public virtual Employee Employee { get; set; }
        public bool isDeleted { get; set; }
    }
}
