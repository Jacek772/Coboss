using Coboss.Types.DTO;
using Coboss.Types.Exceptions;

namespace Coboss.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new ErrorDTO
                {
                    Key = "BAD_REQUEST",
                    Message = ex.Message
                });
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new ErrorDTO
                {
                    Key = "NOT_FOUND",
                    Message = ex.Message
                });
            }
            catch
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new ErrorDTO
                {
                    Key = "SERWER_ERROR",
                    Message = "Serwer error"
                });
            }
        }
    }
}
