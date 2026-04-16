using Hospital.Core.Contracts;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

public class CityService : ICityService
{
    private readonly IWebHostEnvironment env;

    public CityService(IWebHostEnvironment env)
    {
        this.env = env;
    }

    public async Task<List<string>> GetAllAsync()
    {
        var path = Path.Combine(env.WebRootPath, "data", "cities.json");

        if (!File.Exists(path))
        {
            return new List<string>();
        }

        var json = await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<List<string>>(json)
               ?? new List<string>();
    }
}