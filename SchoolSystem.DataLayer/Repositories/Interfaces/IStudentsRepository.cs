using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.DataLayer.Repositories.Interfaces
{
    public interface IStudentsRepository : IGenericRepository<StudentEntity>
    {
        Task<IEnumerable<StudentEntity>> GetAllStudentsOrderedByUsernameAsync();
        Task<StudentEntity> GetStudentByHisLogin(string login);

        Task<StudentEntity> VerifyUsersCredentials(string login, string password);
    }
}