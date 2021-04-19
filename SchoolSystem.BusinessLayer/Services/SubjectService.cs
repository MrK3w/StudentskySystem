using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.DataLayer;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.BusinessLayer.Services
{
    public class SubjectService
    {
        public async Task<IEnumerable<SubjectEntity>> GetListOfSubjects()
        {
            using var unitOfWork = new UnitOfWork();
            var subject = await unitOfWork.Subjects.GetAllSubjectsAsync();
            return subject;
        }

        public async Task UpdateSubject(SubjectEntity subject)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Subjects.UpdateAsync(subject, x => x.Id == subject.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task DeleteSubject(SubjectEntity subject)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Subjects.RemoveAsync(x => x.Id == subject.Id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public async Task CreateSubject(SubjectEntity subject)
        {
            using var unitOfWork = new UnitOfWork();
            try
            {
                await unitOfWork.Subjects.AddAsync(subject);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
