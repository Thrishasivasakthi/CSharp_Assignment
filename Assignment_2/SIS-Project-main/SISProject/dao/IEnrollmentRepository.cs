using System.Collections.Generic;
using assignment_2.entity;

namespace assignment_2.dao
{
    public interface IEnrollmentRepository
    {
        void AddEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int enrollmentId);
        List<Enrollment> GetAllEnrollments();
    }
}
