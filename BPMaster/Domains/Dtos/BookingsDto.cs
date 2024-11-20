using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class BookingsDto
    {
        public Guid Id { get; set; }
        public Guid roomid { get; set; }
        public string customername { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int status { get; set; }
        public string note { get; set; } = string.Empty;
    }
}