using System;

namespace assignment_2.entity
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment(int paymentId, Student student, decimal amount, DateTime paymentDate)
        {
            PaymentId = paymentId;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        public Student GetStudent()
        {
            return Student;
        }

        public decimal GetPaymentAmount()
        {
            return Amount;
        }

        public DateTime GetPaymentDate()
        {
            return PaymentDate;
        }
    }
}
