using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthorization();

//Admin Check Middleware Start
app.Use(async (context, next) => {

    string[] route = (context.Request.Path.ToString()+"/").Split("/");
    
    if(route[1]=="Admin")
    {
        if(route[2]!="Login" && route[2] != "Login2F")
        {
            if (context.Request.Cookies["user_auth"] != null)
            {
                await next.Invoke();
                return;
            }
            else
            {
                context.Response.Redirect("/Admin/Login");
                return;
            }
        }
    }

    await next.Invoke();

});
//Admin Check Middleware End


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
