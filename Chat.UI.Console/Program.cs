using System.Net.Http.Json;

var x = Console.ReadLine();

var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7110/");

var response = await client.PostAsJsonAsync("authenticate", new { });
var response2 = await client.PostAsJsonAsync("register", new { });
