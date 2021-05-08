
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolSystem.WebApplication.Constants;

namespace SchoolSystem.WebApplication.Entities
{
    [Table(TableNames.LESSON_PLAN, Schema = "school")]
    public class LessonEntity
    {
        [Key]
        [Column(ColumnNames.LessonPlanTable.ID)]
        [Required]
        
        public int Id { get; set;}
        
        [Column(ColumnNames.LessonPlanTable.START_TIME)]
        [Required]
        public TimeSpan StartTime { get; set;}

        [Column(ColumnNames.LessonPlanTable.END_TIME)]
        [Required]
        public TimeSpan EndTime { get; set; }

        [Column(ColumnNames.LessonPlanTable.SUBJECT_ID)]
        [Required]
        public int SubjectId { get; set; }
        
        [ForeignKey("SubjectId")]
        [Required]
        public SubjectEntity Subject { get; set; }
        
        [Column(ColumnNames.LessonPlanTable.DAY_OF_WEEK_ID)]
        [Required]
        public DayOfTheWeekEntity Day
        {
            get;
            set;
        }
        
    }
}