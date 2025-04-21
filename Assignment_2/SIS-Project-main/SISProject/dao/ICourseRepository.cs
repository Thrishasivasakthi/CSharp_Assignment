using System.Collections.Generic;
using assignment_2.entity;

namespace assignment_2.dao
{
    public interface ICourseRepository
    {
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
        Course GetCourseById(int courseId);
        List<Course> GetAllCourses();
    }
}
