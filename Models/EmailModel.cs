using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
    public class EmailModel
    {
        [Required][Range(0, 15000, ErrorMessage = ("Wykładowca musi zostać wybrany!"))]
        public int SupervisorID { get; set; }
        public string Title { get; set; }
        public string MailText { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
