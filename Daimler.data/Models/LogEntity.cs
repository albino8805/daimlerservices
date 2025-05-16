using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Models
{
    public class LogEntity : BaseEntity
    {
        [Required]
        public int CreatedBy { get; set; }

        
        public DateTime CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
