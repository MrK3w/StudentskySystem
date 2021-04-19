using SchoolSystem.DataLayer.Entities;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer
{
    public interface IUnitOfWork
    {
        IStudentsRepository Students { get; }
        OrmEntitySet<StudentEntity> Student { get; }

    }
}
