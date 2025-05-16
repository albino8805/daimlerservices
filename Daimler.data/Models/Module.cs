using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Models
{
    public class Module : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(350)]
        public string Description { get; set; } = null!;
    }
}
