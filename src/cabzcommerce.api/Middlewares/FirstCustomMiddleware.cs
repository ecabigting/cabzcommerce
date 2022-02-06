namespace cabzcommerce.api.Middlewares
{
    public class FirstCustomMiddleware 
    {
        private readonly RequestDelegate next;
        
        public FirstCustomMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // await ReadRequestBody(httpContext);

                await next(httpContext);
            }
            catch (Exception ex)
            {
                
            }
        }

        // private async Task ReadRequestBody(HttpContext httpContext)
        // {
        //     // read request body
        //     httpContext.Request.EnableBuffering();

        //     using (var reader = new StreamReader(
        //         httpContext.Request.Body,
        //         encoding: Encoding.UTF8,
        //         detectEncodingFromByteOrderMarks: false,
        //         bufferSize: 1024,
        //         leaveOpen: true
        //         ))
        //     {
        //         requestBody = await reader.ReadToEndAsync();

        //         // reset the request body stream position so that next middleware can read it
        //         httpContext.Request.Body.Position = 0;
        //     }
        // }

    }
}