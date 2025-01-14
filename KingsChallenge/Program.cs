using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, Kings Challenge project!");
string url = "https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings"; // Replace with your URL
using HttpClient client = new();
try
{
    Console.WriteLine("Fetching data...");

    // Send GET request
    HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode(); // Throw an exception if the status code is not 2xx

    // Read the response content as a string
    string responseData = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Data fetched successfully!");

    // Deserialize the JSON array into a List of MyItem objects
    var kings = JsonSerializer.Deserialize<List<King>>(responseData);

    // Iterate over the list and process each item
    foreach (var king in kings)
    {
        Console.WriteLine($"Id: {king.Id}, Name: {king.Name}, Country: {king.Country}, House: {king.House}, Years: {king.Years}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

public class King
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("nm")]
    public string Name { get; set; }
    [JsonPropertyName("cty")]
    public string Country { get; set; }
    [JsonPropertyName("hse")]
    public string House { get; set; }
    [JsonPropertyName("yrs")]
    public string Years { get; set; }
}
