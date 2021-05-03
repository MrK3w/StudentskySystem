using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolSystem.WebApplication.Constants;

namespace SchoolSystem.WebApplication.Entities
{
    [Table(TableNames.STUDENT_TABLE, Schema = "school")]
    public class StudentEntity
    {
        [Key]
        [Column(ColumnNames.StudentTable.ID)]
        public int Id { get; set;}

        [Column(ColumnNames.StudentTable.FIRST_NAME)]
        public string FirstName { get; set;}

        [Column(ColumnNames.StudentTable.LAST_NAME)]
        public string LastName { get; set; }

        [Column(ColumnNames.StudentTable.LOGIN)]
        public string Login { get; set;}

        [Column(ColumnNames.StudentTable.PASSWORD)]
        public string Password { get; set; }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}