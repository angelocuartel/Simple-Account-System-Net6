using Sentry;

namespace SimpleAccountSystem.Mvc.Attributes
{
    public class GlobalExceptionHandlerMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);

                SentrySdk.CaptureMessage("Captured via middleware global exception handler.");
                SentrySdk.CaptureMessage($"{ex.Message}");

                await context.Response.WriteAsJsonAsync("Cannot proceed, something went wrong on the server side.");
            }
        }
    }
}
