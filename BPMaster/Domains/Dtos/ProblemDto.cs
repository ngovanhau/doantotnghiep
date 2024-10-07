using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class ProblemDto
    {
        [Required]
        public string room_name { get; set; } = string.Empty;
        [Required]
        public string problem { get; set; } = string.Empty;
        public string decription { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public int fatal_level { get; set; }
        public int status { get; set; } = 0;
    }
}

