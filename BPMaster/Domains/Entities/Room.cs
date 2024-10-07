
using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("room")]
    public class Room : SystemLogEntity<Guid>
    {
        public string room_name { get; set; } = string.Empty;
        public decimal room_price { get; set; }
        public int floor { get; set; } 
        public int number_of_bedrooms { get; set; } 
        public int number_of_living_rooms { get; set; } 
        public decimal acreage { get; set; }
        public int limited_occupancy { get; set; }
        public decimal deposit { get; set; } 
        public int renter { get; set; } 
        public string service { get; set; } = string.Empty ;
        public string image { get; set; } = string.Empty;
        public string utilities { get; set; } = string.Empty;
        public string interior { get; set; } = string.Empty;    
        public string describe { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public Guid Building_Id { get; set; }
    }
}
