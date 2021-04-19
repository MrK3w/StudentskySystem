using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer.Repositories
{
    public class TeachersRepository : GenericRepository<TeacherEntity>, ITeachersRepository
    {
        public TeachersRepository(OrmEntitySet<TeacherEntity> context) : base(context)
        {
        }

        public async Task<IEnumerable<TeacherEntity>> GetAllTeachersOrderedByUsernameAsync()
        {
            var teachers = await GetAllAsync();
            return teachers.OrderBy(teacher => teacher.LastName).ToList();
        }

        public async Task<TeacherEntity> VerifyTeacherCredentials(string login, string password)
        {
            var teach = await FirstOrDefaultAsync(teacher => teacher.Login == login && teacher.Password == password);
            return teach;
        }
    }
}