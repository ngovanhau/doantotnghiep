using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class TransactionGroupDto
    {
        public int type { get; set; }
        public string name { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
    }
}
