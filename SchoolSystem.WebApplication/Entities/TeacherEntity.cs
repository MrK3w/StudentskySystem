using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolSystem.WebApplication.Constants;

namespace SchoolSystem.WebApplication.Entities
{
    [Table(TableNames.TEACHER_TABLE, Schema = "school")]
    public class TeacherEntity
    {
            [Key]
            [Column(ColumnNames.TeacherTable.ID)]
            public int Id { get; set; }

            [Column(ColumnNames.TeacherTable.FIRST_NAME)]
            public string FirstName { get; set; }

            [Column(ColumnNames.TeacherTable.LAST_NAME)]
            public string LastName { get; set; }

            [Column(ColumnNames.TeacherTable.LOGIN)]
            public string Login { get; set; }

            [Column(ColumnNames.TeacherTable.PASSWORD)]
            public string Password { get; set; }

            [Column(ColumnNames.TeacherTable.PHONE_NUMBER)]
            public string PhoneNumber { get; set; }

            public override string ToString()
            {
                return $"{FirstName} {LastName}";
            }
    }
}