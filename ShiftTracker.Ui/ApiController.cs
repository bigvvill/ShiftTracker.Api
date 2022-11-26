using Newtonsoft.Json;
using RestSharp;
using ShiftTracker.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTracker.Ui
{
    internal class ApiController
    {
        public List<Shift> GetShifts()
        {
            var client = new RestClient("https://localhost:7170/api");
            var request = new RestRequest("/Shifts");
            var response = client.ExecuteAsync(request);

            List<Shift> shifts = new List<Shift>();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Shifts>(rawResponse);

                shifts = serialize.ShiftList;

                TableFormat.ShowTable(shifts, "Shifts");
                return shifts;
            }

            return shifts;
        }
    }
}
