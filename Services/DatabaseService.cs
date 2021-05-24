using Microsoft.EntityFrameworkCore;
using ScheduleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Services
{
    public class DatabaseService
    {
        public ApplicationDbContext Context { get; set; }

        public DatabaseService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<List<Supervisor>> GetSupervisors()
        {
            return await Context.Supervisors.ToListAsync();
        }

        public async Task<Supervisor> GetSupervisorByID(int id)
        {
            return (await Context.Supervisors.FirstOrDefaultAsync(s => s.Id == id));
        }

        public async Task<List<StudentData>> GetStudents()
        {
            return await Context.Students.ToListAsync();
        }

        public async Task FillStudentDatabase(List<StudentData> students)
        {
            foreach (StudentData student in students)
            {
                await Context.Students.AddAsync(student);
            }
            await Context.SaveChangesAsync();
        } 

        public async Task<int> YeetStudents()
        {
            foreach (StudentData student in Context.Students)
            {
                Context.Students.Remove(student);
            }
            return await Context.SaveChangesAsync();
        }
    }
}
