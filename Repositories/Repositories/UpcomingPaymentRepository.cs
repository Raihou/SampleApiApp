using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Repositories
{
    public class UpcomingPaymentRepository : IUpcomingPaymentRepository
    {
        private readonly string _connectionString;

        public UpcomingPaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private SqlConnection GetAndOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void AddNewPayment(UpcomingPayment payment)
        {
            var param = new DynamicParameters();
            param.Add("@pAmount", payment.Amount);
            param.Add("@pDueDate", payment.DueDate);
            param.Add("@pPaymentDescription", payment.Description);
            using(var db = GetAndOpenConnection())
            {
                db.Execute("dbo.uspAddNewUpcomingPayment", param, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<UpcomingPayment> GetAllPayments()
        {
            using(var db = GetAndOpenConnection())
            {
                return db.Query<UpcomingPayment>("dbo.uspGetAllUpcomingPayments", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void MarkPaymentAsPaid(int paymentId)
        {
            var param = new DynamicParameters();
            param.Add("@pPaymentId", paymentId);
            using(var db = GetAndOpenConnection())
            {
                db.Execute("dbo.uspMarkAsPaid", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
