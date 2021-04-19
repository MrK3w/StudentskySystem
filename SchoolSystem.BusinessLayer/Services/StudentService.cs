using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.BusinessLayer.Services
{
    public class StudentService
    {
        public async Task<IEnumerable<StudentEntity>> GetListOfAllStudents()
        {
            using var unitOfWork = new UnitOfWork();
            var students = await unitOfWork.Students.GetAllStudentsOrderedByUsernameAsync();
            return students;
        }

        public async Task<StudentEntity> VerifyStudentCredentials(string login, string password)
        {
            using var unitOfWork = new UnitOfWork();
            var student = await unitOfWork.Students.VerifyUsersCredentials(login, password);
            return student;
        }

        public async Task InsertStudent(StudentEntity student)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Students.AddAsync(student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task UpdateStudent(StudentEntity student)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Students.UpdateAsync(student, stud => stud.Id == student.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteStudent(StudentEntity student)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Students.RemoveAsync(stud => stud.Id == student.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
