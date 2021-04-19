using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.DataLayer.Repositories.Interfaces
{
    public interface IStudentsLessonRepository : IGenericRepository<StudentsLessonEntity>
    {
        Task<IEnumerable<StudentsLessonEntity>> GetAllStudentsPlans();
    }
}