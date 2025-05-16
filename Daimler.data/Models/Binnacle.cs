using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Models
{
    public class Binnacle : BaseEntity
    {
        [Required]
        [ForeignKey("UserFKNavigation")]
        public int UserFK { get; set; }

        [Required]
        [ForeignKey("ModuleFKNavigation")]
        public int ModuleFK { get; set; }

        [Required]
        [MaxLength(600)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
