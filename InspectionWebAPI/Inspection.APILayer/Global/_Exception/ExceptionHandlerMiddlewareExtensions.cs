namespace Inspection.APILayer.Global._Exception
{
  public static class ExceptionHandlerMiddlewareExtensions
  {
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
      app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
  }
}
