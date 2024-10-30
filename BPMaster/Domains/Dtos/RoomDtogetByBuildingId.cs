using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class RoomDtogetByBuildingId
    {
        public Guid Id { get; set; }
        [Required]
        public string room_name { get; set; } = string.Empty;
        public int status { get; set; }
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
        public List<RoomserviceDto> roomservice { get; set; } = new List<RoomserviceDto>();
        public List<string> imageUrls { get; set; } = new List<string>();
        public string utilities { get; set; } = string.Empty;
        public string interior { get; set; } = string.Empty;
        public string describe { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        [Required]
        public Guid? Building_Id { get; set; } = null;
        public int TenantCount { get; set; }
    }
}

