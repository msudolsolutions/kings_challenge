// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Kings Challenge project!");
var kings = new List<King>();

public class King
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string House { get; set; }
    public string Years { get; set; }
}
