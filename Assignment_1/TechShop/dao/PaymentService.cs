using System;
using System.Collections.Generic;
using TechShop.entity;

namespace TechShop.dao
{
    public class PaymentService
    {
        private static List<Payment> payments = new List<Payment>();
        private static int paymentIdCounter = 1;

        public void ProcessPayment(Payment payment)
        {
            payment.PaymentID = paymentIdCounter++;
            payments.Add(payment);
        }

        public List<Payment> GetAllPayments()
        {
            return payments;
        }

        public List<Payment> GetPaymentsByOrderId(int orderId)
        {
            return payments.FindAll(p => p.OrderID == orderId);
        }

        public decimal GetTotalPayments()
        {
            decimal total = 0;
            foreach (var payment in payments)
            {
                total += payment.Amount;
            }
            return total;
        }
    }
}