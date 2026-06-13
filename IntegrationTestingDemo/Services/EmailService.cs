namespace IntegrationTestingDemo.Services;

public class EmailService : IEmailService
{
    public async Task SendOrderConfirmationAsync(string email, string orderDetails)
    {
        // Simulate sending an email
        // (In a real application, SendGrid or SMTP would be used)
        await Task.Delay(100); // Simulate network latency

        Console.WriteLine($"Email sent to {email}: {orderDetails}");
    }
}