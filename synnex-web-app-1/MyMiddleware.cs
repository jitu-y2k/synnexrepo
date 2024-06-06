using System;
namespace synnex_web_app_1
{
	public class MyMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _param1;
		public MyMiddleware(RequestDelegate next, string param1)
		{
			_next = next;
			_param1 = param1;
		}

        public MyMiddleware(string param1)
        {
           
        }

        public RequestDelegate Next => _next;

        public async Task Invoke(HttpContext context)
		{
			if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
			{
				context.Response.ContentType = "text/plain";
				await context.Response.WriteAsync($"This is first middleware - {_param1}\n");
			}
            await _next(context);
            await context.Response.WriteAsync("This is post all middlewares - in first middleware\n");
        }
	}
}

