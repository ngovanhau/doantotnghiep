using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class CustomerDto
    {
        [Required]
        public string customer_name { get; set; } = string.Empty; 
        [Required]
        public string phone_number { get; set; } = string.Empty;
        public string Choose_room { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string date_of_birth { get; set; } = string.Empty;
        public string CCCD { get; set; } = string.Empty;
        public DateTime date_of_issue { get; set; }
        public string place_of_issue { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string image_CCCD { get; set; } = string.Empty;
    }
}
