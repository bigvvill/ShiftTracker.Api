using Newtonsoft.Json;
using RestSharp;
using ShiftTracker.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTracker.Ui.Services;

public class ShiftServiceUi
{
    private RestClient client = new("https://localhost:7170/api/");

    public void GetShifts()
    {
        var request = new RestRequest("Shifts");
        var response = client.Execute(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Content;

            var serialize = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);
            for (int i = 0; i< serialize.Count; i++)
            {
                serialize[i].Pay = Math.Round(serialize[i].Pay, 2);
            }            

            TableFormat.ShowTable(serialize, "Shifts");

        }
    }

    public RestResponse<Shift> GetShiftById(int id)
    {

        var request = new RestRequest($"Shifts/{id}");
        var response = client.Execute<Shift>(request);
        return response;
    }

    public void AddShift(Shift shift)
    {
        var request = new RestRequest("Shifts", Method.Post);
        request.AddJsonBody(JsonConvert.SerializeObject(shift));
        var response = client.Execute<Shift>(request);
        
    }

    public RestResponse<Shift> DeleteShift(int id)
    {
        var request = new RestRequest($"Shifts/{id}", Method.Delete);
        var response = client.Execute<Shift>(request);
        
        return response;
    }

    public void UpdateShift(Shift shift)
    {
        var request = new RestRequest($"Shifts/{shift.ShiftId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(shift));
        var response = client.Execute<Shift>(request);
        
    }

    internal void GetPayPeriod(List<DateTime> payPeriodDates)
    {
        var request = new RestRequest("Shifts");
        var response = client.Execute(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Content;
            decimal grossPay = 0;
            decimal minutesWorked = 0;
            decimal hoursWorked = 0;

            var serialize = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);
            for (int i = 0; i < serialize.Count; i++)
            {
                serialize[i].Pay = Math.Round(serialize[i].Pay, 2);
            }

            List<Shift> payPeriod = new();

            for (int j = 0; j < serialize.Count; j++)
            {
                if (serialize[j].Start >= payPeriodDates[0] && serialize[j].End <= payPeriodDates[1])
                {
                    payPeriod.Add(serialize[j]);
                    grossPay += serialize[j].Pay;
                    minutesWorked += serialize[j].Minutes;
                }           
            }

            if (payPeriod.Count == 0)
            {
                Console.WriteLine("\nNo records found.");
                Console.ReadLine();
                GetUserInput getUserInput = new GetUserInput();
                getUserInput.MainMenu();
            }

            TableFormat.ShowTable(payPeriod, "Shifts");

            hoursWorked = minutesWorked / 60;
            hoursWorked = Math.Round(hoursWorked, 2);            

            Console.WriteLine($"Gross Pay: ${grossPay}");
            Console.WriteLine($"Hours: {hoursWorked}");
        }
    }
}

