using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.DbConstants;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer.Repositories
{
    public class LessonPlanRepository : GenericRepository<LessonPlanEntity>, ILessonPlanRepository
    {
        public LessonPlanRepository(OrmEntitySet<LessonPlanEntity> context) : base(context)
        {
        }

        public async Task<IEnumerable<LessonPlanEntity>> GetAllLessonPlanAsync()
        {
            return await Task.Run(() => Context
                .Join(
                    ColumnNames.SubjectTable.ID,
                    ColumnNames.LessonPlanTable.SUBJECT_ID,
                    typeof(SubjectEntity),typeof(LessonPlanEntity)).
                Join(ColumnNames.TeacherTable.ID, ColumnNames.SubjectTable.TEACHER_ID, 
                typeof(TeacherEntity),typeof(SubjectEntity)).Get());
        }

        public async Task<IEnumerable<LessonPlanEntity>> GetAllLessonPlanAsyncOfStudent(int id)
        {
            return await Task.Run(() => Context
                .Join(
                    ColumnNames.SubjectTable.ID,
                    ColumnNames.LessonPlanTable.SUBJECT_ID,
                    typeof(SubjectEntity), typeof(LessonPlanEntity)).
                Join(ColumnNames.TeacherTable.ID, ColumnNames.SubjectTable.TEACHER_ID,
                    typeof(TeacherEntity), typeof(SubjectEntity)).Get());
        }

    }
}
