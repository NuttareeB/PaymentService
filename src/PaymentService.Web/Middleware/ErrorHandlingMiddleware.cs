namespace ProductReliableCollectionService.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                throw ex;
                //await HandleExceptionAsync(context, ex);
            }
        }

    }
}
