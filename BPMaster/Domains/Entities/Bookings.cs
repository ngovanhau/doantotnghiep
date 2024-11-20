using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("bookings")]
    public class Bookings : SystemLogEntity<Guid>
    {
        public Guid roomid { get; set; }
        public string customername { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int status { get; set; }
        public string note { get; set; } = string.Empty;
    }
}
