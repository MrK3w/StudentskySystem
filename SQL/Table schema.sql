create table day_of_week
(
    id  int identity
        constraint day_of_week_pk
            primary key nonclustered,
    day varchar(10) not null
)
go

create unique index day_of_week_day_uindex
    on day_of_week (day)
go

create table students
(
    student_id int identity
        primary key,
    first_name varchar(60) not null,
    last_name  varchar(60) not null,
    login      varchar(7)  not null,
    password   varchar(60) not null
)
go

create table teachers
(
    teacher_id   int identity
        constraint teachers_pk
            primary key nonclustered,
    first_name   varchar(60) not null,
    last_name    varchar(60) not null,
    login        varchar(40) not null,
    password     varchar(60) not null,
    phone_number varchar(15) not null
)
go

create table subjects
(
    subject_id int identity
        constraint subjects_pk
            primary key nonclustered,
    name       varchar(40)   not null,
    teacher_id int
        constraint subjects_teachers_teacher_id_fk
            references teachers
            on delete set null,
    credits    int default 1 not null
)
go

create table lesson_plan
(
    id             int identity
        constraint lesson_plan_pk
            primary key nonclustered,
    start_time     time not null,
    end_time       time not null,
    subject_id     int  not null
        constraint lesson_plan_subjects_subject_id_fk
            references subjects,
    day_of_week_id int  not null
        constraint lesson_plan_day_fk
            references day_of_week,
    teacher_id     int  not null
        constraint lesson_plan_teachers_teacher_id_fk
            references teachers
)
go

create unique index lesson_plan_day_of_week_id_unikat
    on lesson_plan (day_of_week_id, teacher_id, start_time)
go

create table students_lesson
(
    student_lesson_id int identity
        constraint students_lesson_pk
            primary key nonclustered,
    lesson_plan_id    int not null
        constraint students_lesson_lesson_plan_id_fk
            references lesson_plan,
    student_id        int not null
        constraint students_lesson_students_student_id_fk
            references students
)
go

create unique index students_lesson_lesson_plan_id_uindex
    on students_lesson (lesson_plan_id, student_id)
go

