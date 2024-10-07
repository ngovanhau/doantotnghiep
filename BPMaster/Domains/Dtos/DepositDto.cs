using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class DepositDto
    {
        [Required]
        public decimal deposit_amount { get; set; }
        [Required]
        public string room { get; set; } = string.Empty;
        [Required]
        public DateTime move_in_date { get; set; }
        [Required]
        public string payment_method { get; set; } = string.Empty;
        [Required]
        public string depositor { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
    }
}

