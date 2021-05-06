using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using SchoolSystem.WebApplication.Constants;

namespace SchoolSystem.WebApplication.Entities
{
    [Table(TableNames.SUBJECT_TABLE, Schema = "school")]
    public class SubjectEntity
    {
        [Key]
        [Column(ColumnNames.SubjectTable.ID)]
        public int Id { get; set; }

        [Column(ColumnNames.SubjectTable.NAME)]
        public string Name { get; set; }
        
        [Column(ColumnNames.SubjectTable.TEACHER_ID)]
        public int HeadTeacherId { get; set; }
        
        [ForeignKey("HeadTeacherId")]
        public TeacherEntity HeadTeacher { get; set; }

        [Column(ColumnNames.SubjectTable.CREDITS)]
        public int Credits { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}