using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Daimler.data.Models
{
    public class User : LogEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Password { get; set; } = null!;

        [MaxLength(20)]
        public string Phone { get; set; } = null!;

        [Required]
        [ForeignKey("ProfileFKNavigation")]
        public int ProfileFK { get; set; }

        public virtual Profile ProfileFKNavigation { get; set; } = null!;
    }
}
