using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class DashboardDto
    {
        public int building { get; set; }
        public int customer { get; set; }
        public int contract { get; set; }
        public int problem { get; set; }
        public int room { get; set; }
    }
}
