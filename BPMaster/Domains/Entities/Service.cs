using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("service")]
    public class Service : SystemLogEntity<Guid>
    {
        public string service_name { get; set; } = string.Empty;
        public string collect_fees { get; set; } = string.Empty;
        public string unitMeasure { get; set; } = string.Empty;
        public int service_cost { get; set; }
        public string image { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
    }
}
