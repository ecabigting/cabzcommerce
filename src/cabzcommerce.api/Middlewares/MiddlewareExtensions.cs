namespace cabzcommerce.api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirstCustomMiddleware>();
        }
    }    
}