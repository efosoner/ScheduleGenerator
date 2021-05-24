using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Data;
using ScheduleApp.Models;
using ScheduleApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Controllers
{
    public class ScheduleController : Controller
    {
        public ScheduleController(DatabaseService databaseService, ExcelService excelService)
        {
            this.databaseService = databaseService;
            this.excelService = excelService;
        }

        public DatabaseService databaseService { get; set; }
        public ExcelService excelService { get; set; }

        // GET: ScheduleController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadXLSX(FileUploadModel fileUploadModel) 
        {
            List<StudentData> students = excelService.GetFileContent(fileUploadModel.File);
            await databaseService.FillStudentDatabase(students);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearDataset()
        {
            int modifiedRows = await databaseService.YeetStudents();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleGenerator()
        {
            await databaseService.GetStudents();
            return View();
        }
    }
}
