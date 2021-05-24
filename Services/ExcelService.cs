using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using ScheduleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Services
{
    public class ExcelService
    {  
        public List<StudentData> GetFileContent(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            List<StudentData> students = new List<StudentData>();

            using (var reader = ExcelReaderFactory.CreateReader(file.OpenReadStream()))
            {
                while (reader.Read())
                {
                    StudentData studentData = new StudentData
                    {
                        LastName = reader.GetValue(1).ToString(),
                        FirstName = reader.GetValue(2).ToString(),
                        Index = reader.GetValue(3).ToString(),
                        Specialization = reader.GetValue(4).ToString(),
                        Group = reader.GetValue(5).ToString(),
                        Subject = reader.GetValue(6).ToString(),
                        Supervisor = reader.GetValue(7).ToString(),
                        Reviewer = reader.GetValue(8)?.ToString()
                    };
                    students.Add(studentData);
                }
            }
            return students;
        }
    }
}
