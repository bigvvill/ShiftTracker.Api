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

            TableFormat.ShowTable(serialize, "Shifts");

        }
    }

    public RestResponse<Shift> GetShiftById(int id)
    {

        var request = new RestRequest($"shifts/{id}");
        var response = client.Execute<Shift>(request);
        return response;
    }

    public void AddShift(Shift shift)
    {
        var request = new RestRequest("shifts", Method.Post);
        request.AddJsonBody(JsonConvert.SerializeObject(shift));
        var response = client.Execute<Shift>(request);
        Console.WriteLine(response.Content);
    }

    public RestResponse<Shift> DeleteShift(int id)
    {
        var request = new RestRequest($"shifts/{id}", Method.Delete);
        var response = client.Execute<Shift>(request);
        Console.WriteLine(response.Content);
        return response;
    }

    public void UpdateShift(Shift shift)
    {
        var request = new RestRequest($"Shifts/{shift.ShiftId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(shift));
        var response = client.Execute<Shift>(request);
        Console.WriteLine(response.Content);
    }
}

