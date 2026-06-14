using Microsoft.AspNetCore.Identity;

namespace IntegrationTestingDemo.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}