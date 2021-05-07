using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolSystem.WebApplication.Constants;

namespace SchoolSystem.WebApplication.Entities
{
    [Table(TableNames.USER_TABLE, Schema = "school")]
    public class UserEntity
    {
        [Key]
        [Column(ColumnNames.UserTable.ID)]
        public int Id { get; set;}

        [Column(ColumnNames.UserTable.FIRST_NAME)]
        public string FirstName { get; set;}

        [Column(ColumnNames.UserTable.LAST_NAME)]
        public string LastName { get; set; }

        [Column(ColumnNames.UserTable.LOGIN)]
        public string Login { get; set;}

        [Column(ColumnNames.UserTable.PASSWORD)]
        public string Password { get; set; }
        
        [Column(ColumnNames.UserTable.TYPE)]
        public string TypeOfUser { get; set; }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}