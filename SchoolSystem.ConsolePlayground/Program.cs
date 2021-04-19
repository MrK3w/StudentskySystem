using System.Data.SqlClient;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.ConsolePlayground
{
    static class Program
    {
        static void Main()
        {
            //
            var sqlConnection = new SqlConnection(
                "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Initial Catalog=school_database"
            );
            
            sqlConnection.Open();
            OrmEntitySet<StudentsLessonEntity> subjectsSet = new OrmEntitySet<StudentsLessonEntity>(sqlConnection);
            subjectsSet.Where(x => x.Id == 5).Delete();
        }
    }
}