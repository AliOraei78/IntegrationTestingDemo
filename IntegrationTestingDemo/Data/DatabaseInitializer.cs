using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTestingDemo.Data;

public class DatabaseInitializer
{
    private readonly AppDbContext _context;

    public DatabaseInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }
}