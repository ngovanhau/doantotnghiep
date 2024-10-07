using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("problem")]
    public class Problem : SystemLogEntity<Guid>
    {
        public string room_name { get; set; } = string.Empty;
        public string problem { get; set; } = string.Empty;
        public string decription { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public int fatal_level { get; set; } 
        public int status { get; set; } = 0;
    }
}


