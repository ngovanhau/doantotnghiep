
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class IncomeExpenseGroupDto
    {
        public Guid Id { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public Guid transactiongroupid { get; set; }
        public string transactiongroupname { get; set; } = string.Empty;
        public string paymentmethod { get; set; } = string.Empty;
        public Guid contractid { get; set; }
        public string contractname { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
    }
}

