using synnex_web_app_1;

var builder = WebApplication.CreateBuilder(args);

//Configuring services - that you want to use as dependency injection
//ConfigureServices
builder.Services.Configure<Person>(config =>
{

});
//builder.Services.Configure<Person>(config =>
//{
//    config.Name = "Ramesh";
//    config.Age = 30;
//});


WebApplication app = builder.Build();

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

app.Map("/mybranch", (mybranch) =>
{
    mybranch.Use(async (context, next) =>
    {
        //if (context.Request.Method = HttpMethods.Get && context.Request)
        if (!context.Response.HasStarted)
        {
            context.Response.ContentType = "text/plain";
        }
        await context.Response.WriteAsync("This is branching middleware\n");
        await next();
    });

    mybranch.Run(new MyMiddleware("abc").Invoke);
    //mybranch.Use(async (context, next) =>
    //{
    //    await next();
    //});
    //mybranch.Use(async (context, next) =>
    //{
    //    await next();
    //});
});

app.UseMiddleware<MyMiddleware>("Custom Parameter");

app.Map("/mybranch1", (mybranch1) =>
{
    mybranch1.Use(async (context, next) =>
    {
        await next();
    });
    mybranch1.Use(async (context, next) =>
    {
        await next();
    });
    mybranch1.Use(async (context, next) =>
    {
        await next();
    });
});



//app.MapGet("/", () => "Hello World!");

app.MapGet("/", MyHelloMethod);

app.MapGet("/abc", () => "This is my abc path middleware");

app.Run();

string MyHelloMethod()
{
    return "Hello World\n";
}