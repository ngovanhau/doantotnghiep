using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class BuildingDto
    {
        public string building_name { get; set; } = string.Empty;
        public string number_of_floors { get; set; } = string.Empty;
        public string rental_costs { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string district { get; set; } = string.Empty;
        public DateTime payment_date { get; set; }
        public int advance_notice { get; set; }
        public DateTime payment_time { get; set; }
        public DateTime payment_timeout { get; set; }
        public string management { get; set; } = string.Empty;
        public string fee_based_service { get; set; } = string.Empty;
        public string free_service { get; set; } = string.Empty;
        public string utilities { get; set; } = string.Empty;
        public string building_note { get; set; } = string.Empty;
    }
}
