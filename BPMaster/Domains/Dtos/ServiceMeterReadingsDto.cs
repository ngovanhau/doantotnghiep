using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class ServiceMeterReadingsDto
    {
        public Guid Id { get; set; }
        public string building_name { get; set; } = string.Empty;
        public Guid building_id { get; set; }
        public string room_name { get; set; } = string.Empty;
        public Guid room_id { get; set; }
        public int status { get; set; }
        public string recorded_by { get; set; } = string.Empty;
        public Guid recordid { get; set; }
        public DateTime record_date { get; set; }
        public decimal electricity_old { get; set; }
        public decimal electricity_new { get; set; }
        public decimal electricity_price { get; set; }
        public decimal electricity_cost { get; set; }
        public decimal water_old { get; set; }
        public decimal water_new { get; set; }
        public decimal water_price { get; set; }
        public decimal water_cost { get; set; } 
        public bool confirmation_status { get; set; }
        public decimal  total_amount { get; set; }

    }
}
