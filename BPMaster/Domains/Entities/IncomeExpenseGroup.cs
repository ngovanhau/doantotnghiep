using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("incomeexpensegroup")]
    public class IncomeExpenseGroup : SystemLogEntity<Guid>
    {
        public DateTime date { get; set; }
        public int amount { get; set; }
        public Guid transactiongroupid { get; set; }
        public string transactiongroupname { get; set; } = string.Empty;
        public string paymentmethod { get; set; } = string.Empty ;
        public Guid contractid { get; set; }
        public string contractname { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
    }
}