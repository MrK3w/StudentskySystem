using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolSystem.WebApplication.Constants;

namespace SchoolSystem.WebApplication.Entities
{
    [Table(TableNames.USER_TABLE, Schema = "school")]
    public class UserEntity
    {
        public UserEntity()
        {
            TypeOfUser = "student";
        }

        [Key]
        [Column(ColumnNames.UserTable.ID)]
        [Required]
        public int Id { get; set;}

        [Column(ColumnNames.UserTable.FIRST_NAME)]
        [MaxLength(60)]
        [Required]
        public string FirstName { get; set;}

        [Column(ColumnNames.UserTable.LAST_NAME)]
        [MaxLength(60)]
        [Required]
        public string LastName { get; set; }

        [Column(ColumnNames.UserTable.LOGIN)]
        [MaxLength(7)]
        [Required]
        public string Login { get; set;}

        [Column(ColumnNames.UserTable.PASSWORD)]
        [MaxLength(60)]
        [Required]
        public string Password { get; set; }
        
        [Required] 
        [Column(ColumnNames.UserTable.TYPE)]
        public string TypeOfUser { get; set; }
        
        public string FullName => FirstName + " " + LastName;
        
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}