using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("deposit")]
    public class Deposit : SystemLogEntity<Guid>
    {
        public decimal deposit_amount { get; set; }
        public string room { get; set; } = string.Empty;
        public DateTime move_in_date { get; set; } 
        public string payment_method { get; set; } = string.Empty;
        public string depositor { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
    }
}
