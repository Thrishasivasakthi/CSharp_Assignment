using System;
using System.Collections.Generic;
using System.Linq;
using TechShop.exception;

public class Payment
{
    public int PaymentID { get; set; }
    public int OrderID { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string? PaymentMethod { get; internal set; }
    public DateTime PaymentDate { get; internal set; }
}

public class PaymentManager
{
    private List<Payment> payments = new List<Payment>();

    public void RecordPayment(Payment payment)
    {
        payments.Add(payment);
    }

    public void UpdatePaymentStatus(int paymentId, string status)
    {
        var p = payments.FirstOrDefault(p => p.PaymentID == paymentId);
        if (p != null) p.Status = status;
        else throw new PaymentFailedException("Payment not found.");
    }

    public List<Payment> GetAllPayments() => payments;
}