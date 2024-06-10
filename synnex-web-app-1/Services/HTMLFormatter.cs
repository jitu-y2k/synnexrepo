using System;
using System.Diagnostics.Metrics;

namespace synnex_web_app_1.Services
{
	public class HTMLFormatter: IFormatter
	{
        private int _counter = 0;

        public async Task FormatResponse(HttpContext context, string content)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/html";
            }
            await context.Response.WriteAsync($"Counter : {++_counter} ,Formatted text - <h1>{content}<h1>");
        }
    }
}

