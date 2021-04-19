using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SchoolSystem.DataLayer.DbConstants;

namespace SchoolSystem.DataLayer.Entities
{
    [Table(TableNames.LESSON_PLAN, Schema = "school")]
    public class LessonPlanEntity
    {
        [Key]
        [Column(ColumnNames.LessonPlanTable.ID)]
        public int Id
        {
            get;
            set;
        }

        [Column(ColumnNames.LessonPlanTable.START_TIME)]
        public TimeSpan StartTime
        {
            get;
            set;
        }

        [Column(ColumnNames.LessonPlanTable.END_TIME)]
        public TimeSpan EndTime
        {
            get;
            set;
        }

        [Column(ColumnNames.LessonPlanTable.SUBJECT_ID)]
        [ForeignKey("Subjects")]
        public SubjectEntity Subject
        {
            get; 
            set;
        }

        [Column(ColumnNames.LessonPlanTable.TEACHER_ID)]
        [ForeignKey("Teachers")]
        public TeacherEntity Teacher { get; set; }

        [Column(ColumnNames.LessonPlanTable.DAY_OF_WEEK_ID)]
        public DayOfTheWeekEntity Day
        {
            get;
            set;
        }
        
    }
}
