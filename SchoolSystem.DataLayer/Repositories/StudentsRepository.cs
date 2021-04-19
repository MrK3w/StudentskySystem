using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer.Repositories
{
    public class StudentsRepository : GenericRepository<StudentEntity>, IStudentsRepository
    {
        public StudentsRepository(OrmEntitySet<StudentEntity> context) : base(context)
        {
        }
 
        public async Task<IEnumerable<StudentEntity>> GetAllStudentsOrderedByUsernameAsync()
        {
            var students = await GetAllAsync();
            return students.OrderBy(student => student.LastName).ToList();
        }

        public async Task<StudentEntity> GetStudentByHisLogin(string login)
        {
            return await FirstOrDefaultAsync(student => student.Login == login);
        }

        public async Task<StudentEntity> VerifyUsersCredentials(string login, string password)
        {
            var stud = await FirstOrDefaultAsync(student => student.Login == login && student.Password == password);
            return stud;
        }
    }
}