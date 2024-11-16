using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("Bill")]
    public class Bill : SystemLogEntity<Guid>
    {
        public int status { get; set; }
        public DateTime date { get; set; }
        public Guid roomid { get; set; }
        public string roomname { get; set; } =string.Empty;
        public DateTime payment_date { get; set; }  
        public DateTime due_date { get; set; }
        public int cost_room {  get; set; }
        public int cost_service { get; set; }
        public int total_amount {  get; set; }
        public int penalty_amount { get; set; }
        public int  discount { get; set; }
        public int  final_amount { get; set; }
        public string note { get; set; } = string.Empty;
    }
}
