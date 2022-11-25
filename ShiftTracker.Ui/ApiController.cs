using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTracker.Ui
{
    internal class ApiController
    {
        public async Task GetShiftsAsync()
        {
            using HttpClient client = new()
            {
                BaseAddress = new Uri("https://localhost:7104/api/")
            };

            int id = 1;

            Shift? user = await client.GetFromJsonAsync<Shift>($"Shifts/{id}");
            Console.WriteLine($"Id: {user?.ShiftId}");
            Console.WriteLine($"Name: {user?.Start}");
            Console.WriteLine($"Username: {user?.End}");
            Console.WriteLine($"Email: {user?.Pay}");
            Console.WriteLine($"Email: {user?.Minutes}");
            Console.WriteLine($"Email: {user?.Location}");

            Console.ReadLine();

            // Post a new user.

            Shift newShift = new();
            newShift.Start = DateTime.Parse("2022-11-14 09:50:00");
            newShift.End = DateTime.Now;
            newShift.Pay = 30.0000m;
            newShift.Minutes = 630.0000m;
            newShift.Location = "Lexington";
            HttpResponseMessage response = await client.PostAsJsonAsync("Shifts", newShift);
            Console.WriteLine(
                $"{(response.IsSuccessStatusCode ? "Success" : "Error")} - {response.StatusCode}");
            Console.ReadLine();
        }
    }
}
