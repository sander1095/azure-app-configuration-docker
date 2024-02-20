using Azure.Core;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);


// PREREQUISITES: 
// 1: You should have App Configuration Data Reader/App Configuration Data Owner role on your App Configuration instance.
// 2: You're running this in Docker using Visual Studio (haven't tested VSCode/Rider)
// 3: You have set up Azure Service Authentication (https://learn.microsoft.com/en-us/dotnet/azure/configure-visual-studio)

var azureAppConfigurationConnectionString = "YOUR_ACA_ENDPOINT_HERE";
TokenCredential tokenCredential = builder.Environment.IsDevelopment() ? 
        // ManagedIdentityCredential must be EXCLUDED when running in Docker to prevent errors.
        // During development we want to use our DefaultAzureCredential (like visual studio/vscode/etc..)
        new DefaultAzureCredential(new DefaultAzureCredentialOptions() { ExcludeManagedIdentityCredential = true }) : 
        new ManagedIdentityCredential(); // On non-local envs we use Managed Identity

builder.Configuration.AddAzureAppConfiguration(x => x.Connect(new Uri(azureAppConfigurationConnectionString), tokenCredential));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
