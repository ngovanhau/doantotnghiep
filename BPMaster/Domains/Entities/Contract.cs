using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("contract")]
    public class Contract : SystemLogEntity<Guid>
    {
        public string contract_name { get; set; } = string.Empty;
        public string room { get; set; } = string.Empty;
        public Guid roomId { get; set; }
        public DateTime start_day { get; set; }
        public DateTime end_day { get; set; }
        public DateTime billing_start_date { get; set; } 
        public int payment_term { get; set; }
        public int room_fee { get; set; }
        public int deposit{ get; set; }
        public Guid CustomerId {  get; set; } 
        public string service { get; set; } = string.Empty;
        public string clause { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
    }
}

