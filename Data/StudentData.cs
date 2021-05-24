using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Data
{
    public class StudentData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Index { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public string Group { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Supervisor { get; set; }
        public string Reviewer { get; set; }
    }
}
