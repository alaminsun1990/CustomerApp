using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();

        try
        {
            var response = await client.GetAsync("http://localhost:8080/api/customers");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response from API:");
            Console.WriteLine(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API call failed: {ex.Message}");
        }
    }
}
