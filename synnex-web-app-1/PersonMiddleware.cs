using System;
using Microsoft.Extensions.Options;

namespace synnex_web_app_1
{
	public class PersonMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly Person _person;

        public PersonMiddleware(RequestDelegate next, IOptions<Person> personInfo)
		{
            _next = next;
            _person = personInfo.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/plain";
            }
            await context.Response.WriteAsync($"Person Information : Name - {_person.Name}, Age - {_person.Age}");
            await _next(context);
        }
	}
}

