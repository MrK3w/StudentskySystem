using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.BusinessLayer.Services
{
    public class StudentsLessonService
    {
        public async Task<IEnumerable<StudentsLessonEntity>> GetListOfAllPlans()
        {
            using var unitOfWork = new UnitOfWork();
            var plans = await unitOfWork.StudentsLessonPlans.GetAllStudentsPlans();
            return plans;
        }

        public async Task Delete(StudentsLessonEntity lesson)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.StudentsLessonPlans.RemoveAsync(x => x.Id == lesson.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public async Task Insert(StudentsLessonEntity lesson)
        {
            using var unitOfWork = new UnitOfWork();
            await unitOfWork.StudentsLessonPlans.AddAsync(lesson);
        }
    }
}
