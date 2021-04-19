using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.DataLayer.Repositories.Interfaces
{
    public interface ISubjectRepository : IGenericRepository<SubjectEntity>
    {
        Task<IEnumerable<SubjectEntity>> GetAllSubjectsAsync();

    }
}
