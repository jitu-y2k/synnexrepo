using System;
namespace synnex_web_app_1.Services
{
	public interface IFormatter
	{
		Task FormatResponse(HttpContext context, string content);
	}
}

