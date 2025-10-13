using WebApplication1.Data;

namespace College_App.Data.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();

        public Task<Student> GetByIdAsync(int id, bool useNoTracking = false);

        public Task<Student> GetByNameAsync(string name);

        Task<int> CreateAsync(Student student);
        Task<int> UpdateAsync(Student student);

        Task<bool> DeleteAsync(Student student);
    }
}
