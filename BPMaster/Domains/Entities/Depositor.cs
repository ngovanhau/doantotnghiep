using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("depositor")]
    public class Depositor : SystemLogEntity<Guid>
    {
        public string depositor_name { get; set; } = string.Empty;
        public string phone_number { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public int ID_number { get; set; }
        public string address { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
    }
}


