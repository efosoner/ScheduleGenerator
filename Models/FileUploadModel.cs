using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
    public class FileUploadModel
    {
        [Required(ErrorMessage = "Plik musi zostać wczytany przed załadowaniem!")]
        [FileExtensions]
        [BindProperty]
        public IFormFile File { get; set; }
    }
}
