using System;

namespace synnex_web_app_1.Services
{
	public class TextFormatter:IFormatter
	{
        private int _counter = 0;

        public async Task FormatResponse(HttpContext context, string content)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/plain";
            }
            await context.Response.WriteAsync($"Counter : {++_counter} ,Formatted text - {content}");
        }
    }
}

