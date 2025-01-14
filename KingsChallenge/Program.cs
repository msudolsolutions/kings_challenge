using System.Text.Json;

Console.WriteLine("Hello, Kings Challenge project!");
var kings = new List<King>();
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

    // Parse JSON and handle array root
    using JsonDocument jsonDocument = JsonDocument.Parse(responseData);
    var root = jsonDocument.RootElement;

    if (root.ValueKind == JsonValueKind.Array)
    {
        Console.WriteLine("Root element is an array. Iterating over elements:");

        foreach (var item in root.EnumerateArray())
        {
            Console.WriteLine(item.ToString());
        }
    }
    else
    {
        Console.WriteLine("Root element is not an array:");
        Console.WriteLine(root.ToString());
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

public class King
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string House { get; set; }
    public string Years { get; set; }
}
