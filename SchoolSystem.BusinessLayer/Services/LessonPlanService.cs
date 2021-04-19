using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.BusinessLayer.Services
{
    public class LessonPlanService
    {
        public async Task<IEnumerable<LessonPlanEntity>> GetListOfAllPlans()
        {
            using var unitOfWork = new UnitOfWork();
            var plans = await unitOfWork.LessonPlans.GetAllLessonPlanAsync();
            return plans;
        }

        public async Task<IEnumerable<LessonPlanEntity>> GetListOfAllPlansOfStudent(int id)
        {
            using var unitOfWork = new UnitOfWork();
            var plans = await unitOfWork.LessonPlans.GetAllLessonPlanAsyncOfStudent(id);
            return plans;
        }

        public async Task InsertPlan(LessonPlanEntity lessonPlan)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.LessonPlans.AddAsync(lessonPlan);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdatePlan(LessonPlanEntity lessonPlan)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.LessonPlans.UpdateAsync(lessonPlan, lesson => lesson.Id == lessonPlan.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePlan(LessonPlanEntity lessonPlan)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.LessonPlans.RemoveAsync(x => x.Id == lessonPlan.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
