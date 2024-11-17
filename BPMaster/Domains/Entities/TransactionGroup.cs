﻿using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("TransactionGroup")]
    public class TransactionGroup : SystemLogEntity<Guid>
    {
        public int type { get; set; }
        public string name { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
    }
}