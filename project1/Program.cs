using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YourNamespace.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<DocumentService>();

//chave de licença da Syncfusion
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQyMjg5NEAzMjM2MmUzMDJlMzBRcHFROHI1cHMvK0gyRjFyOHJtaTNHaUEzeEw5Ulh6M20wTmMvdmtaWjJjPQ==;Mgo+DSMBaFt5QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRbQlVjQX5VckFiX31feHI=;Mgo+DSMBPh8sVXJyS0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9nSXZTfkVlXXtfdnZWQmM=;Mgo+DSMBMAY9C3t2U1hhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5bdk1jX3tacXJXRWRf;MzQyMjg5OEAzMjM2MmUzMDJlMzBQK0JITGVHemlIUml1NzU4MU8vRHNXRDRXQ2RGK2ViaWlCVnRveHZ0eFpVPQ==;MzQyMjg5OUAzMjM2MmUzMDJlMzBHNmEzVmdIb1ZyM1JBRTNnTWNMWnlCaDgramNSWE5zN29mZ0wxMVAwODV3PQ==");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Document}/{action=Index}/{id?}");

app.Run();