namespace SchoolSystem.DataLayer.DbConstants
{
    public static class ColumnNames
    {
        public static class StudentTable
        {
            public const string ID = "student_id";
            public const string FIRST_NAME = "first_name";
            public const string LAST_NAME = "last_name";
            public const string LOGIN = "login";
            public const string PASSWORD = "password";
        }

        public static class TeacherTable
        {
            public const string ID = "teacher_id";
            public const string FIRST_NAME = "first_name";
            public const string LAST_NAME = "last_name";
            public const string LOGIN = "login";
            public const string PASSWORD = "password";
            public const string PHONE_NUMBER = "phone_number";
        }

        public static class SubjectTable
        {
            public const string ID = "subject_id";
            public const string NAME = "name";
            public const string TEACHER_ID = "teacher_id";
            public const string CREDITS = "credits";
        }

        public static class LessonPlanTable
        {
            public const string ID = "id";
            public const string START_TIME = "start_time";
            public const string END_TIME = "end_time";
            public const string SUBJECT_ID = "subject_id";
            public const string DAY_OF_WEEK_ID = "day_of_week_id";
            public const string TEACHER_ID = "teacher_id";
        }

        public static class StudentsLessonTable
        {
            public const string ID = "student_lesson_id";
            public const string LESSON_ID = "lesson_plan_id";
            public const string STUDENT_ID = "student_id";
        }
    }
}
