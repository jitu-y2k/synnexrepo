using Microsoft.Extensions.Options;
using synnex_web_app_1;
using synnex_web_app_1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromHours(8);
});



//Configuring services - that you want to use as dependency injection
//ConfigureServices
//builder.Services.Configure<Person>(config =>
//{
//    config.Name = "Karan";
//    config.Age = 35;
//});

//builder.Services.Configure<Person>(builder.Configuration.GetSection("PersonInfo"));
//builder.Services.Configure<Person>(config =>
//{
//    config.Name = "Ramesh";
//    config.Age = 30;
//});

builder.Services.AddTransient<IFormatter, HTMLFormatter>();

//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(15);
//    options.Cookie.IsEssential = true;
//});


WebApplication app = builder.Build();

app.UseHsts();

app.MapGet("/checkHttps", (HttpContext context) =>
{
    context.Response.WriteAsync($"Is Https Request : {context.Request.IsHttps}");
});

//app.UseHttpsRedirection();

//if (app.Environment.IsProduction())
//{

//}



//app.UseSession();

//app.MapGet("/sessionInfo", async (HttpContext context) =>
//{
//    Console.WriteLine(context.Session.GetInt32("counter"));

//    var counter = (context.Session.GetInt32("counter")??0) + 1;

//    Console.WriteLine("Counter : {0}", counter);

//    context.Session.SetInt32("counter", counter);

//    await context.Session.CommitAsync();

//    await context.Response.WriteAsync($"Session Info : {counter}");

//});


//app.MapGet("/personinfo", (HttpContext context, IOptions<Person> personOptions) =>
//{
//    var person = personOptions.Value;
//    context.Response.WriteAsync($"Person Info Name: {person.Name}, Age: {person.Age}");
//});

//app.MapGet("/personinfo", (HttpContext context, ILogger<Person> logger) =>
//{
//    //var personInfoSection = configuration.GetSection("PersonInfo");

//    //personInfoSection.GetValue<string>

//    logger.LogDebug("");

//    logger.LogCritical("");

//    var person = new Person
//    {
//        Name = configuration["PersonInfo:Name"],  //configuration.GetValue<string>("PersonInfo:Name"),
//        Age = configuration.GetValue<int>("PersonInfo:Age")
//    };

//    context.Response.WriteAsync($"Person Info Name: {person.Name}, Age: {person.Age}");
//});

//app.MapGet("/cookieInfo", (HttpContext context) =>
//{
//    //var counter = context.Request.Cookies.FirstOrDefault(c => c.Key == "Counter").Value??"0";
//    var counter = Int32.Parse(context.Request.Cookies["Counter"] ?? "0") + 1;

//    context.Response.Cookies.Append(
//        "Counter",
//        counter.ToString(),
//        new CookieOptions()
//        {
//             MaxAge = TimeSpan.FromMinutes(30)
//        });


//    context.Response.WriteAsync($"Cookie Value : {counter}");

//});

//app.MapGet("/clearCookies", (HttpContext context) =>
//{
//    context.Response.Cookies.Delete("Counter");
//    context.Response.Redirect("/");
//});


app.MapGet("/person", async (HttpContext context) =>
{
    if (!context.Response.HasStarted)
    {
        context.Response.ContentType = "text/plain";
    }

    var person = new Person();
    person.Name = "Rahul";
    person.Age = 25;

    await context.Response.WriteAsync($"This is my person information : {person.Name}, Age: {person.Age}");
});

//app.UseMiddleware<PersonMiddleware>();

//this is the area where you will specify your middlewares
//Configure

//Middlewares are of two types:
//1. Use Middleware - non terminal
//2. Run Middleware - Terminal middleware
//3. Map - terminal middleware

//Middlewares

// Middleware1() - Use
//Middleware2() - Terminal

//app.Use(async (context, next) =>
//{
//    //It will do some process here
//    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//    {
//        context.Response.ContentType = "text/plain";
//        await context.Response.WriteAsync("This is first middleware\n");
//    }
//    else
//    {
//        await next();
//        await context.Response.WriteAsync("This is post all middlewares - in first middleware\n");
//    }

//});


//app.Use(async (context, next) =>
//{
//    //It will do some process here
//    //if (context.Request.Method = HttpMethods.Get && context.Request)
//    if (!context.Response.HasStarted)
//    {
//        context.Response.ContentType = "text/plain";
//    }
//    await context.Response.WriteAsync("This is my second middleware\n");
//    await next();
//    await context.Response.WriteAsync("This is post all middlewares - in second middleware\n");
//});

//app.UseMiddleware<MyMiddleware>();
//app.UseMiddleware(typeof(MyMiddleware));

//app.UseMiddleware<MyMiddleware>("Custom Parameter");

//app.Map("/mybranch", (mybranch) =>
//{
//    mybranch.Use(async (context, next) =>
//    {
//        //if (context.Request.Method = HttpMethods.Get && context.Request)
//        if (!context.Response.HasStarted)
//        {
//            context.Response.ContentType = "text/plain";
//        }
//        await context.Response.WriteAsync("This is branching middleware\n");
//        await next();
//    });

//    mybranch.Run(new MyMiddleware("abc").Invoke);
//    //mybranch.Use(async (context, next) =>
//    //{
//    //    await next();
//    //});
//    //mybranch.Use(async (context, next) =>
//    //{
//    //    await next();
//    //});
//});

//app.UseMiddleware<MyMiddleware>("Custom Parameter");

//app.Map("/mybranch1", (mybranch1) =>
//{
//    mybranch1.Use(async (context, next) =>
//    {
//        await next();
//    });
//    mybranch1.Use(async (context, next) =>
//    {
//        await next();
//    });
//    mybranch1.Use(async (context, next) =>
//    {
//        await next();
//    });
//});

//var formatter = new TextFormatter();

//app.MapGet("/", () => "Hello World!");

app.Map("/textformatter", async (HttpContext context, IFormatter formatter1, IFormatter formatter2) =>
{
    //var formatter = new TextFormatter();

    await formatter1.FormatResponse(context, "This is my formatter1");
    await formatter2.FormatResponse(context, "This is my formatter2");

});

app.MapGet("/", MyHelloMethod);

app.MapGet("/abc", () => "This is my abc path middleware");

app.Run();

string MyHelloMethod()
{
    return "Hello World\n";
}


//interface INotification
//{
//    void SendNotification();
//}

//class EmailService : INotification
//{
//    public void SendNotification()
//    {
//        Console.WriteLine("Send Email");
//    }
//}

//class WhatsAppService : INotification
//{
//    public void SendNotification()
//    {
//        Console.WriteLine("Send Whatsapp message");
//    }
//}

//class SMSService : INotification
//{
//    public void SendNotification()
//    {
//        Console.WriteLine("Send SMS message");
//    }
//}


//void ConfigureNotification(INotification notificationService){
//    _notificationService = notificationService;
//}