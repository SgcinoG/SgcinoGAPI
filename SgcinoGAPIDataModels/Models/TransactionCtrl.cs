using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataModels.Models
{
    public class TransactionCtrl
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public long MeterNumber { get; set; }
        public string CduId { get; set; }
    }

    public enum TransactionStatus
    {
        Pending = 1,
        Success,
        Fail,
        Timeout,
        Error,
        Blocked
    }
}
