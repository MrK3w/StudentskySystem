
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
        public int Id { get; set;}

        [Column(ColumnNames.LessonPlanTable.START_TIME)]
        public TimeSpan StartTime { get; set;}

        [Column(ColumnNames.LessonPlanTable.END_TIME)]
        public TimeSpan EndTime { get; set; }

        [Column(ColumnNames.LessonPlanTable.SUBJECT_ID)]
        public int SubjectId { get; set; }
        
        [ForeignKey("SubjectId")]
        public SubjectEntity Subject { get; set; }
        
        [Column(ColumnNames.LessonPlanTable.DAY_OF_WEEK_ID)]
        public DayOfTheWeekEntity Day
        {
            get;
            set;
        }
        
    }
}