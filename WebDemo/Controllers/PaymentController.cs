using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebDemo.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpPost]
        public async Task<PaymentResponse> Pay(Payment value)
        {
            try
            {
                PaymentManager paymentManager = new PaymentManager();
                value.TransactionId = Guid.NewGuid();
                value.OrderId = Guid.NewGuid();
                var paymentResponse = paymentManager.Pay(value);

                return paymentResponse;
            }
            catch (Exception ex)
            {
                return new PaymentResponse()
                {
                    Response = ex.Message,
                    IsSuccessful = false
                };
            }
        }

        [HttpPost]
        public async Task<PaymentResponse> PayUyi(Payment value)
        {
            try
            {
                PaymentManager paymentManager = new PaymentManager();
                value.TransactionId = Guid.NewGuid();
                value.OrderId = Guid.NewGuid();
                var paymentResponse = paymentManager.Pay(value);

                return paymentResponse;
            }
            catch (Exception ex)
            {
                return new PaymentResponse()
                {
                    Response = ex.Message,
                    IsSuccessful = false
                };
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Result(string value)
        {
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
    }
}
