using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.DbConstants;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer.Repositories
{
    public class SubjectRepository : GenericRepository<SubjectEntity>, ISubjectRepository
    {
        public SubjectRepository(OrmEntitySet<SubjectEntity> context) : base(context)
        {
        }

        public async Task<IEnumerable<SubjectEntity>> GetAllSubjectsAsync()
        {
            return await Task.Run(() => Context.Join(
                ColumnNames.TeacherTable.ID,
                ColumnNames.SubjectTable.TEACHER_ID,
                typeof(TeacherEntity),typeof(SubjectEntity)).Get());
        }

    }
}
