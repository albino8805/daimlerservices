using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Models
{
    public class Folder : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int? CreatedBy { get; set; }
    }
}
