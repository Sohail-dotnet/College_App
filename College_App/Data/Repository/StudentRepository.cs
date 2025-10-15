using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace College_App.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student> ,IStudentRepository
    {
        private readonly CollegeDBContext _dbcontext;
        public StudentRepository(CollegeDBContext dBContext) : base(dBContext) 
            { _dbcontext = dBContext; }

        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus)
        {
            //write code to return the pending status of students
            return null;

        }
    }
}
