using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("Dashboard")]
    public class Dashboard : SystemLogEntity<Guid>
    {
        public int building { get; set; }
        public int customer { get; set; }
        public int contract { get; set; }
        public int problem { get; set; }
        public int room { get; set; }
    }
}
