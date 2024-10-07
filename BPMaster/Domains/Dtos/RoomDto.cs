using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class RoomDto
    {
        [Required]
        public string room_name { get; set; } = string.Empty;
        [Required]
        public decimal room_price { get; set; }
        [Required]
        public int floor { get; set; }
        [Required]
        public int number_of_bedrooms { get; set; }
        [Required]
        public int number_of_living_rooms { get; set; }
        [Required]
        public decimal acreage { get; set; }
        [Required]
        public int limited_occupancy { get; set; }
        public decimal deposit { get; set; }
        public int renter { get; set; }
        public string service { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string utilities { get; set; } = string.Empty;
        public string interior { get; set; } = string.Empty;
        public string describe { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
    }
}
