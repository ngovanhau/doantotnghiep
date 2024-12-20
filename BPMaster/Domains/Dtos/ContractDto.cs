﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class ContractDto
    {
        public Guid Id { get; set; }
        [Required]
        public string contract_name { get; set; } = string.Empty;
        public int status { get; set; }
        public string rentalManagement { get; set; } = string.Empty;
        [Required]
        public string room { get; set; } = string.Empty;
        public Guid roomId { get; set; }
        [Required]
        public DateTime start_day { get; set; }
        [Required]
        public DateTime end_day { get; set; }
        [Required]
        public DateTime billing_start_date { get; set; }
        [Required]
        public int payment_term { get; set; } 
        [Required]
        public int room_fee { get; set; }
        [Required]
        public int deposit { get; set; }
        public Guid CustomerId { get; set; }
        public string service { get; set; } = string.Empty;
        public string clause { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
    }
}
