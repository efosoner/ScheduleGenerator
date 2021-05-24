using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Data
{
    public class Supervisor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        // Problem stringa rozwiąże nam GUI
        public string Degree { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        
        [NotMapped]
        public string FullName => this.ToString();

        public override string ToString()
        {
            return Degree + " " + FirstName + " " + LastName;
        }
    }
}
