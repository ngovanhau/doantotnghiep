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
        public Guid Id { get; set; }
        [Required]
        public decimal deposit_amount { get; set; }
        [Required]
        public Guid roomid { get; set; } 
        public string roomname { get; set; } = string.Empty;    
        [Required]
        public DateTime move_in_date { get; set; }
        [Required]
        public string payment_method { get; set; } = string.Empty;
        [Required]
        public Guid Customerid { get; set; } 
        public string Customername { get; set; } = string.Empty;
        public List<string> image { get; set; } = new List<string>();
        public string note { get; set; } = string.Empty;
        public int status { get; set; } 
    }
}

