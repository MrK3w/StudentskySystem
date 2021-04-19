using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SchoolSystem.DataLayer;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.BusinessLayer.Services
{
    public class TeacherService
    {
        public async Task<IEnumerable<TeacherEntity>> GetListOfAllTeachers()
        {
            using var unitOfWork = new UnitOfWork();
            var teachers = await unitOfWork.Teachers.GetAllTeachersOrderedByUsernameAsync();
            return teachers;
        }

        public async Task<TeacherEntity> VerifyTeacherCredentials(string login, string password)
        {
            using var unitOfWork = new UnitOfWork();
            var teacher = await unitOfWork.Teachers.VerifyTeacherCredentials(login, password);
            return teacher;
        }

        public async Task InsertTeacher(TeacherEntity teacher)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Teachers.AddAsync(teacher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateTeacher(TeacherEntity teacher)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Teachers.UpdateAsync(teacher, x => x.Id == teacher.Id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteTeacher(TeacherEntity teacher)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Teachers.RemoveAsync(x => x.Id == teacher.Id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}

