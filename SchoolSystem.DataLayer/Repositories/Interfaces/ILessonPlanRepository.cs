using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.DataLayer.Repositories.Interfaces
{
    public interface ILessonPlanRepository : IGenericRepository<LessonPlanEntity>
    {
        Task<IEnumerable<LessonPlanEntity>> GetAllLessonPlanAsync();

        Task<IEnumerable<LessonPlanEntity>> GetAllLessonPlanAsyncOfStudent(int id);
    }
}
