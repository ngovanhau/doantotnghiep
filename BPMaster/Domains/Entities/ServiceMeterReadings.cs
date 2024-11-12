using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("ServiceMeterReadings")]
    public class ServiceMeterReadings : SystemLogEntity<Guid>
    {
        public string building_name { get; set; } = string.Empty;
        public Guid building_id { get; set; }
        public string room_name { get; set; } = string.Empty;
        public Guid room_id { get; set; }
        public int status {  get; set; }
        public string recorded_by { get; set; } = string.Empty;
        public DateTime record_date { get; set; }
        public decimal  electricity_old { get; set; }
        public decimal  electricity_new { get; set; } 
        public decimal  electricity_price { get; set; }
        public decimal electricity_cost { get; set; }
        public decimal  water_old { get; set; } 
        public decimal  water_new { get; set;} 
        public decimal  water_price { get; set;}
        public decimal water_cost { get; set; }
        public bool confirmation_status { get; set; }
        public decimal  total_amount {  get; set; }
    }
}
