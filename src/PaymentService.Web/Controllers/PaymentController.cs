namespace PaymentService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using PaymentService.Core.Models;
    using PaymentService.Services.Processor;
    using System.ComponentModel.DataAnnotations;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentProcessor _paymentProcessor;
        public PaymentController(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        [HttpPost("processpayment")]
        public ActionResult ProcessPayment([FromBody, Required]JObject request)
        {
            var result = _paymentProcessor.ProcessPayment(request.ToObject<ProcessPaymentRequest>());
            return Ok(result);
        }

        [HttpPost("refund")]
        public ActionResult Refund([FromBody, Required]JObject request)
        {
            var result = _paymentProcessor.Refund(request.ToObject<RefundRequest>());
            return Ok(result);
        }
    }
}
