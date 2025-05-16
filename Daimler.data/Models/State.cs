using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Models
{
    public class State : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey("CountryFKNavigation")]
        public int CountryFK { get; set; }

        public virtual Country CountryFKNavigation { get; set; } = null!;
    }
}
