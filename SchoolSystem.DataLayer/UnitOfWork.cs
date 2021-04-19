using System;
using System.Data.SqlClient;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.DataLayer.Repositories;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Initial Catalog=school_database";
        public IStudentsRepository Students { get; private set; }

        public ITeachersRepository Teachers { get; private set; }
        public ISubjectRepository Subjects { get; private set; }

        public ILessonPlanRepository LessonPlans { get; private set; }
        
        public IStudentsLessonRepository StudentsLessonPlans { get; private set; }

        public OrmEntitySet<StudentEntity> Student { get; }

        private OrmEntitySet<TeacherEntity> Teacher { get; }

        private OrmEntitySet<SubjectEntity> Subject { get; }

        private OrmEntitySet<LessonPlanEntity> LessonPlan { get; }

        private OrmEntitySet<StudentsLessonEntity> StudentsLessonPlan { get; }

        private SqlConnection _sqlConnection; 

        public UnitOfWork()
        {
            SqlConnect();
            Student = new OrmEntitySet<StudentEntity>(_sqlConnection);
            Teacher = new OrmEntitySet<TeacherEntity>(_sqlConnection);
            Subject = new OrmEntitySet<SubjectEntity>(_sqlConnection);
            LessonPlan = new OrmEntitySet<LessonPlanEntity>(_sqlConnection);
            StudentsLessonPlan = new OrmEntitySet<StudentsLessonEntity>(_sqlConnection);
            InitializeAppRepositories();
        }

        private void SqlConnect()
        {
            _sqlConnection = new SqlConnection(
                ConnectionString
            );
            _sqlConnection.Open();
        }

        private void InitializeAppRepositories()
        {
            Students = new StudentsRepository(Student);
            Teachers = new TeachersRepository(Teacher);
            Subjects = new SubjectRepository(Subject);
            LessonPlans = new LessonPlanRepository(LessonPlan);
            StudentsLessonPlans = new StudentsLessonRepository(StudentsLessonPlan);
        }

        public void Dispose()
        {
            _sqlConnection.Close();
        }
    }
}
