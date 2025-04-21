using System;

namespace assignment_2.exception
{
    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException(string message) : base(message) { }
    }

    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(string message) : base(message) { }
    }

    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException(string message) : base(message) { }
    }

    public class TeacherNotFoundException : Exception
    {
        public TeacherNotFoundException(string message) : base(message) { }
    }

    public class PaymentValidationException : Exception
    {
        public PaymentValidationException(string message) : base(message) { }
    }

    public class InvalidStudentDataException : Exception
    {
        public InvalidStudentDataException(string message) : base(message) { }
    }

    public class InvalidCourseDataException : Exception
    {
        public InvalidCourseDataException(string message) : base(message) { }
    }

    public class InvalidEnrollmentDataException : Exception
    {
        public InvalidEnrollmentDataException(string message) : base(message) { }
    }

    public class InvalidTeacherDataException : Exception
    {
        public InvalidTeacherDataException(string message) : base(message) { }
    }

    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }
}
