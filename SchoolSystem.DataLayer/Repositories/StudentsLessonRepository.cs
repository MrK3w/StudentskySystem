using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.DbConstants;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer.Repositories
{
    public class StudentsLessonRepository : GenericRepository<StudentsLessonEntity>, IStudentsLessonRepository
    {
        public StudentsLessonRepository(OrmEntitySet<StudentsLessonEntity> context) : base(context)
        {
        }

        public async Task<IEnumerable<StudentsLessonEntity>> GetAllStudentsPlans()
        {
            return await Task.Run(() => Context.Join(
                    ColumnNames.StudentTable.ID,
                    ColumnNames.StudentsLessonTable.STUDENT_ID,
                    typeof(StudentEntity), typeof(StudentsLessonEntity)).
                Join(ColumnNames.LessonPlanTable.ID, ColumnNames.StudentsLessonTable.LESSON_ID,
                    typeof(LessonPlanEntity), typeof(StudentsLessonEntity)).
                Join(ColumnNames.SubjectTable.ID, ColumnNames.LessonPlanTable.SUBJECT_ID,
                    typeof(SubjectEntity), typeof(LessonPlanEntity)).
                Join(ColumnNames.TeacherTable.ID, ColumnNames.SubjectTable.TEACHER_ID,
                    typeof(TeacherEntity), typeof(SubjectEntity)).
                Get());
        }
    }
}
