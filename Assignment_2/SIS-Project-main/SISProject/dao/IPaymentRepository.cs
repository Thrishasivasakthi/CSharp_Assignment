using System.Collections.Generic;
using assignment_2.entity;

namespace assignment_2.dao
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        List<Payment> GetPaymentsByStudentId(int studentId);
    }
}
