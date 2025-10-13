using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.Data;

namespace College_App.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbcontext;
        private DbSet<T> _dbset;
        public CollegeRepository(CollegeDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbset = _dbcontext.Set<T>();
        }
        public async Task<T> CreateAsync(T dbRecord)
        {
            _dbset.Add(dbRecord);
            await _dbcontext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbset.Remove(dbRecord);
            await _dbcontext.SaveChangesAsync();



            // ✅ Auto reseed identity for SQL Server tables
            var entityType = _dbcontext.Model.FindEntityType(typeof(T));
            var tableName = entityType.GetTableName();
            var keyProperty = entityType.FindPrimaryKey().Properties.First();

            // Only reseed if the key is an int (identity column)
            if (keyProperty.ClrType == typeof(int))
            {
                var maxId = await _dbset.AnyAsync() ? await _dbset.MaxAsync(e => EF.Property<int>(e, keyProperty.Name)) : 0;

                await _dbcontext.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT ('{tableName}', RESEED, {maxId})");
            }


            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbset.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            }
            else
                return await _dbset.Where(filter).FirstOrDefaultAsync();

        }

        public async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbset.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T dbRecord)
        {
            _dbcontext.Update(dbRecord);
            await _dbcontext.SaveChangesAsync();
            return dbRecord;

        }
    }
}
