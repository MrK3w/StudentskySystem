using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.DataLayer.Repositories.Interfaces
{
    public interface ITeachersRepository : IGenericRepository<TeacherEntity>
    {
        Task<IEnumerable<TeacherEntity>> GetAllTeachersOrderedByUsernameAsync();

        Task<TeacherEntity> VerifyTeacherCredentials(string login, string password);
    }
}
