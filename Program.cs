using System;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseKestrel(option => option.AddServerHeader = false);
// Add services to the container.

var couchbaseServer = builder.Configuration["CouchBase"];

builder.Services.AddCouchbase(options => {
 options.ConnectionString = $"couchbase://{couchbaseServer}";
 options.UserName = builder.Configuration["CouchUser"];
 options.Password = builder.Configuration["CouchPass"];
 options.HttpIgnoreRemoteCertificateMismatch = true;
 options.KvIgnoreRemoteCertificateNameMismatch = true;
});

builder.Services.AddCouchbaseBucket<INamedBucketProvider>("FF_Data");

builder.Services.AddSingleton<IQuestionAnswerRepository, QuestionAnswerRepository>();
builder.Services.AddSingleton<IQuestionAnswerContext, QuestionAnswerContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCorsPolicy",
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5400", "https://localhost:5401", "https://localhost:44445");
                      });
});



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();

} else 
{
    app.UseCors("MyCorsPolicy");
}


//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();

