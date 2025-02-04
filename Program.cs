
using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true);

var configuration = builder.Build();

var credential = new AzureKeyCredential(configuration["ModelKey"]);
var endpoint = new Uri(configuration["ModelEndpoint"]);

var chatClient = new ChatCompletionsClient(endpoint, credential)
    .AsChatClient(configuration["ModelName"]);

var question = "If I have 3 apples and eat 2, how many bananas do I have?";
var response = chatClient.CompleteStreamingAsync(question);

Console.WriteLine($">>> User: {question}");
Console.Write(">>>");
Console.WriteLine(">>> DeepSeek (might be a while): ");

await foreach (var item in response)
{
    Console.Write(item);
}
