using OptionPattern.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//E�er app.settings i�erisinden okuyaca��n�z de�erler; IServiceCollection i�ine g�nderilecekse; okuma i�lemini burada yapmal�s�n�z.
//E�er sadece bir middleware i�inde kullanacaksan�z var app = builder.Build(); sat�r�ndan sonra da kullanabilirsiniz.
var setting = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>(); //SmtpSettings t�r�nde �evir.
builder.Services.AddSingleton<SmtpSettings>(setting);

var app = builder.Build();

var appSetting = app.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>(); //serviste de�il de middleware'de.

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
