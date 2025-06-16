using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };

    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- Customer CLI Menu ---");
            Console.WriteLine("1. View all customers");
            Console.WriteLine("2. Add customer");
            Console.WriteLine("3. Delete customer");
            Console.WriteLine("4. Update customer");
            Console.WriteLine("5. Exit");
            Console.Write("Choose option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await GetCustomers();
                    break;
                case "2":
                    await AddCustomer();
                    break;
                case "3":
                    await DeleteCustomer();
                    break;
                case "4":
                    await UpdateCustomer();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }

    private static async Task GetCustomers()
    {
        var customers = await client.GetFromJsonAsync<List<Customer>>("api/customers");
        if (customers is null || customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            return;
        }

        foreach (var c in customers)
        {
            Console.WriteLine($"{c.Id} | {c.FirstName} {c.LastName} | {c.Email} | {c.PhoneNumber}");
        }
    }

    private static async Task AddCustomer()
    {
        var customer = new Customer();

        Console.Write("First Name: ");
        customer.FirstName = Console.ReadLine();

        Console.Write("Middle Name (optional): ");
        customer.MiddleName = Console.ReadLine();

        Console.Write("Last Name: ");
        customer.LastName = Console.ReadLine();

        Console.Write("Email: ");
        customer.Email = Console.ReadLine();

        Console.Write("Phone Number: ");
        customer.PhoneNumber = Console.ReadLine();

        var response = await client.PostAsJsonAsync("api/customers", customer);
        Console.WriteLine(response.IsSuccessStatusCode
            ? "Customer added successfully."
            : $"Failed to add customer. Status: {response.StatusCode}");
    }

    private static async Task DeleteCustomer()
    {
        Console.Write("Enter Customer ID to delete: ");
        var id = Console.ReadLine();

        var response = await client.DeleteAsync($"api/customers/{id}");
        Console.WriteLine(response.IsSuccessStatusCode
            ? "Customer deleted successfully."
            : $"Failed to delete customer. Status: {response.StatusCode}");
    }

    private static async Task UpdateCustomer()
    {
        Console.Write("Enter Customer ID to update: ");
        var id = Console.ReadLine();

        var existing = await client.GetFromJsonAsync<Customer>($"api/customers/{id}");
        if (existing == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        Console.Write($"First Name ({existing.FirstName}): ");
        existing.FirstName = Console.ReadLine() ?? existing.FirstName;

        Console.Write($"Middle Name ({existing.MiddleName}): ");
        existing.MiddleName = Console.ReadLine();

        Console.Write($"Last Name ({existing.LastName}): ");
        existing.LastName = Console.ReadLine() ?? existing.LastName;

        Console.Write($"Email ({existing.Email}): ");
        existing.Email = Console.ReadLine() ?? existing.Email;

        Console.Write($"Phone Number ({existing.PhoneNumber}): ");
        existing.PhoneNumber = Console.ReadLine() ?? existing.PhoneNumber;

        var response = await client.PutAsJsonAsync($"api/customers/{id}", existing);
        Console.WriteLine(response.IsSuccessStatusCode
            ? "Customer updated successfully."
            : $"Failed to update customer. Status: {response.StatusCode}");
    }
}

public class Customer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
