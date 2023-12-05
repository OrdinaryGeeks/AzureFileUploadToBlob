using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using UploadFilesToQuizBowl.Services;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("OGQuizBowlKeyVault")!);
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new ManagedIdentityCredential(builder.Configuration["AZURE_CLIENT_ID"]));



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IBlobConnection, BlobConnection>();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
