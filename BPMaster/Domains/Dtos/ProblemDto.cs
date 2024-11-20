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
        public Guid Id { get; set; }
        [Required]
        public string room_name { get; set; } = string.Empty;
        [Required]
        public string problem { get; set; } = string.Empty;
        public string decription { get; set; } = string.Empty;
        public List<string> image { get; set; } = new List<string>();
        public int fatal_level { get; set; }
        public int status { get; set; } = 0;
        public Guid roomid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

