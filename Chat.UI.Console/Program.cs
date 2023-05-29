using Chat.Commons.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7110/");

//var createUserRequest = new CreateUserRequest("konrad@gmail.com", "Konrad", "Boryś", "password");
//var createUserResponse = await client.PostAsJsonAsync("register", createUserRequest);
//createUserResponse.EnsureSuccessStatusCode();

//var user = await createUserResponse.Content.ReadFromJsonAsync<User>();
//var authRequest = new AuthRequest(user.Email, "password");
//var authResponse = await client.PostAsJsonAsync("authenticate", authRequest);
//authResponse.EnsureSuccessStatusCode();

//var authResponseContent = await authResponse.Content.ReadFromJsonAsync<AuthResponse>();
//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResponseContent.Token);

//var test = await client.GetStringAsync("test");


Console.Write("Enter email: ");
var email = Console.ReadLine();
Console.Write("Enter password: ");
var password = Console.ReadLine();

var response = await client.PostAsJsonAsync("authenticate", new AuthRequest(email, password));
var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

var chatHubClient = new ChatHubClient(authResponse.Token);
chatHubClient.ChatConnection.On<string>("ReceiveMessage", msg =>
{
    Console.WriteLine(msg);
});

Console.WriteLine();
Console.Write("Enter message: ");
var message = Console.ReadLine();

Console.Write("Enter receiver: ");
var receiver = Console.ReadLine();


await chatHubClient.SendMessage(message, receiver);

Console.ReadLine();