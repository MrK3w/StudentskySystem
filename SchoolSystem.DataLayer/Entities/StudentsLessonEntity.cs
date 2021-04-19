using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolSystem.DataLayer.DbConstants;

namespace SchoolSystem.DataLayer.Entities
{
    [Table(TableNames.STUDENTS_LESSON, Schema = "school")]
    public class StudentsLessonEntity
    {
        [Key]
        [Column(ColumnNames.StudentsLessonTable.ID)]
        
        public int Id
        {
            get;
            set;
        }

        [Column(ColumnNames.StudentsLessonTable.LESSON_ID)]
        [ForeignKey("Subject")]
        public LessonPlanEntity LessonPlan

        {
            get;
            set;
        }

        [Column(ColumnNames.StudentsLessonTable.STUDENT_ID)]
        [ForeignKey("Subject")]
        public StudentEntity Student
        {
            get;
            set;
        }


    }
}