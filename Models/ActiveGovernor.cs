using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace csvTask.Models
{
    public class ActiveGovernor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Capital { get; set; }
        
        [Required]
        public string Governor { get; set; }
    }
}
