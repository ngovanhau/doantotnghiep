using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class BillDto
    {
        public string bill_name { get; set; } = string.Empty;
        public int status { get; set; }
        public int status_payment { get; set; }
        public Guid building_id { get; set; }
        public string customer_name { get; set; } = string.Empty;
        public Guid customer_id { get; set; }
        public DateTime date { get; set; }
        public Guid roomid { get; set; }
        public string roomname { get; set; } = string.Empty;
        public DateTime payment_date { get; set; }
        public DateTime due_date { get; set; }
        public int cost_room { get; set; }
        public int cost_service { get; set; }
        public int total_amount { get; set; }
        public int penalty_amount { get; set; }
        public int discount { get; set; }
        public int final_amount { get; set; }
        public string note { get; set; } = string.Empty;
    }
}