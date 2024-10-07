using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class DepositorDto
    {
        [Required]
        public string depositor_name { get; set; } = string.Empty;
        [Required]
        public string phone_number { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public int ID_number { get; set; }
        public string address { get; set; } = string.Empty;
        public string? image { get; set; } = string.Empty;
    }
}


