using System.Collections.Generic;
using assignment_2.entity;

namespace assignment_2.dao
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        Student GetStudentById(int studentId);
        List<Student> GetAllStudents();
    }
}
