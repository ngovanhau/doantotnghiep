using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class ImageCCCD
    {
        public Guid CustomerID { get; set; }
        public string ImageUrl { get; set; }
    }
}

