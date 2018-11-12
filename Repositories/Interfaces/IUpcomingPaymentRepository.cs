using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Repositories
{
    public interface IUpcomingPaymentRepository
    {
        void AddNewPayment(UpcomingPayment payment);
        IEnumerable<UpcomingPayment> GetAllPayments();
        void MarkPaymentAsPaid(int paymentId);
    }
}
