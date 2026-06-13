namespace IntegrationTestingDemo.Services;

public interface IEmailService
{
    Task SendOrderConfirmationAsync(string email, string orderDetails);
}