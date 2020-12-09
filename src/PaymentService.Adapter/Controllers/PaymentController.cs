using Common.AspNetMvc.Core.ServiceProxy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PaymentService.ClientAdapter.ServiceProxy;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
namespace PaymentService.ClientAdapter.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentServiceProxy paymentServiceProxy;
        private readonly HttpClient httpClient;

        public PaymentController(HttpClient httpClient, IPaymentServiceProxy paymentServiceProxy)
        {
            this.httpClient = httpClient;
            this.paymentServiceProxy = paymentServiceProxy;
        }

        [HttpPost("processpayment")]
        public async Task<IActionResult> ProcessPayment([FromBody, Required]JObject jsonData)
        {
            try
            {
                var result = await paymentServiceProxy.ProcessPayment<JToken>(jsonData);
                if (result.IsServiceError) return result.ServiceErrorResult();
                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("refund")]
        public async Task<IActionResult> Refund([FromBody, Required]JObject jsonData)
        {
            try
            {
                var result = await paymentServiceProxy.Refund<JToken>(jsonData);
                if (result.IsServiceError) return result.ServiceErrorResult();
                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
