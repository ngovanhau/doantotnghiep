using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class ServiceDto
    {
        [Required]
        public string service_name { get; set; } = string.Empty;
        [Required]
        public string collect_fees { get; set; } = string.Empty;
        [Required]
        public string unitMeasure { get; set; } = string.Empty;
        public int service_cost { get; set; }
        [Required]
        public string image { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
    }
}
