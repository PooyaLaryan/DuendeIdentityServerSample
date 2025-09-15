using Duende.IdentityModel.Client;
using System.Text.Json;

// step 1: Discover endpoints from metadata
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7094");
if (disco.IsError) { Console.WriteLine(disco.Error); return; }

// step 2: Request a token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new()
{
    Address = disco.TokenEndpoint,
    ClientId = "client",
    ClientSecret = "secret",
    Scope = "api1"
});
if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}
Console.WriteLine("Access Token:" + tokenResponse.AccessToken);

// step 3: Call the API
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken!);
var response = await apiClient.GetAsync("https://localhost:7216/identity"); 
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
    return;
}
var doc = await response.Content.ReadAsStringAsync();
