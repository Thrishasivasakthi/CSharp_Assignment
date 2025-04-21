using assignment_2.entity;

using System;
using System.Collections.Generic;

namespace assignment_2.entity
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string InstructorName { get; set; }
        public List<Enrollment> Enrollments { get; set; }

        public Course(int courseId, string courseName, string courseCode, string instructorName)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
            Enrollments = new List<Enrollment>();
        }

        public void AssignTeacher(Teacher teacher)
        {
            InstructorName = teacher.FirstName + " " + teacher.LastName;
            teacher.AssignedCourses.Add(this);
        }

        public void UpdateCourseInfo(string courseCode, string courseName, string instructor)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            InstructorName = instructor;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"ID: {CourseId}, Name: {CourseName}, Code: {CourseCode}, Instructor: {InstructorName}");
        }

        public List<Enrollment> GetEnrollments()
        {
            return Enrollments;
        }

        public string GetTeacher()
        {
            return InstructorName;
        }
    }
}

