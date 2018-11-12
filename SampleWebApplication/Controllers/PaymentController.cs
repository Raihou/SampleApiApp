using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace SampleWebApplication.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {

        private IUpcomingPaymentRepository _upcomingPaymentRepo;

        public PaymentController(IUpcomingPaymentRepository upcomingPaymentRepository)
        {
            _upcomingPaymentRepo = upcomingPaymentRepository;
        }

        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpPost("AddUpcomingPayment")]
        public IActionResult AddUpcomingPayment([FromBody] UpcomingPayment payment)
        {

            if (ModelState.IsValid)
            {
                _upcomingPaymentRepo.AddNewPayment(payment);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("MarkAsPaid/{paymentId}")]
        public IActionResult MarkAsPaid(int paymentId)
        {
            _upcomingPaymentRepo.MarkPaymentAsPaid(paymentId);
            return Ok();
        }

        [HttpGet("GetAllUpcomingPayments")]
        public IActionResult GetAllUpcomingPayments()
        {
            return Ok(_upcomingPaymentRepo.GetAllPayments());
        }



        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return Ok("value");
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
