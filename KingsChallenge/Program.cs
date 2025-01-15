using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, Kings Challenge project!");

string url = "https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings";
var kings = GetDataFromUrlAsync(url).Result;

/*
1. How many monarchs are there in the list?
2. Which monarch ruled the longest (and for how long)?
3. Which house ruled the longest (and for how long)?
4. What was the most common first name?
*/

Console.WriteLine($"1. There are {kings.Count} monarchs in the list.");
Console.WriteLine($"2. The monarch who ruled the longest is {kings.OrderByDescending(k => k.Years).First().Name} for {kings.OrderByDescending(k => k.Years).First().Years} years.");
Console.WriteLine($"3. The house that ruled the longest is {kings.GroupBy(k => k.House).OrderByDescending(g => g.Count()).First().Key} for {kings.GroupBy(k => k.House).OrderByDescending(g => g.Count()).First().Count()} years.");
Console.WriteLine($"4. The most common first name is {kings.GroupBy(k => k.Name.Split(' ')[0]).OrderByDescending(g => g.Count()).First().Key}.");

static async Task<List<King>> GetDataFromUrlAsync(string url)
{
    List<King> kings = new();
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

        // Deserialize the JSON array into a List of King objects
        kings = JsonSerializer.Deserialize<List<King>>(responseData) ?? new List<King>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
    return kings;
}

public class King
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    [JsonPropertyName("nm")]
    public required string Name { get; set; }
    [JsonPropertyName("cty")]
    public required string Country { get; set; }
    [JsonPropertyName("hse")]
    public required string House { get; set; }
    [JsonPropertyName("yrs")]
    public required string Years { get; set; }
}
