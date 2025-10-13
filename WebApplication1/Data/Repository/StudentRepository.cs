using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace College_App.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbcontext;
        public StudentRepository(CollegeDBContext dBContext) { _dbcontext = dBContext; }


        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbcontext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbcontext.Students.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();
            }
            else
                return await _dbcontext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();

        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _dbcontext.Students.Where(student => student.Name.Contains(name)).FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(Student student)
        {
            _dbcontext.Students.Add(student);
            await _dbcontext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<int> UpdateAsync(Student student)
        {
            _dbcontext.Update(student);
            await _dbcontext.SaveChangesAsync();
            return student.Id;

        }

        public async Task<bool> DeleteAsync(Student student)
        {
            _dbcontext.Students.Remove(student);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
