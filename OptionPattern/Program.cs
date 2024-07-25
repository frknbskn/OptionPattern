using OptionPattern.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Eðer app.settings içerisinden okuyacaðýnýz deðerler; IServiceCollection içine gönderilecekse; okuma iþlemini burada yapmalýsýnýz.
//Eðer sadece bir middleware içinde kullanacaksanýz var app = builder.Build(); satýrýndan sonra da kullanabilirsiniz.
var setting = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>(); //SmtpSettings türünde çevir.
builder.Services.AddSingleton<SmtpSettings>(setting);

var app = builder.Build();

var appSetting = app.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>(); //serviste deðil de middleware'de.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
