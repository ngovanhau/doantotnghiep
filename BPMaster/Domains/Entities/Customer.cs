﻿using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("customer")]
    public class Customer: SystemLogEntity<Guid>
    {
        public string customer_name { get; set; } = string.Empty;
        public string phone_number { get; set; } = string.Empty;
        public Guid choose_room { get; set; } 
        public string email { get; set; } = string.Empty;
        public DateTime date_of_birth { get; set; }
        public string CCCD { get; set; } = string.Empty ;
        public DateTime date_of_issue { get; set; }
        public string place_of_issue { get; set; } = string.Empty;
        public string address {  get; set; } = string.Empty;
        public Guid UserId { get; set; }    
    }
}
