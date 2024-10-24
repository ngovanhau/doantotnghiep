using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class RoomserviceDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
    }
}
