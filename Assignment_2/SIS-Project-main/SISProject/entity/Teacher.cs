using System;
using System.Collections.Generic;

namespace assignment_2.entity
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> AssignedCourses { get; set; }

        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherId = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AssignedCourses = new List<Course>();
        }

        public void UpdateTeacherInfo(string name, string email, string expertise)
        {
            Email = email;
        }

        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"ID: {TeacherId}, Name: {FirstName} {LastName}, Email: {Email}");
        }

        public List<Course> GetAssignedCourses()
        {
            return AssignedCourses;
        }
    }
}
