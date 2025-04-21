using System.Collections.Generic;
using assignment_2.entity;

namespace assignment_2.dao
{
    public interface ITeacherRepository
    {
        void AddTeacher(Teacher teacher);
        void DeleteTeacher(int teacherId);
        List<Teacher> GetAllTeachers();
    }
}
