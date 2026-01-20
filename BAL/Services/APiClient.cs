using Azure;
using DAL.ViewModel;
using Jalaun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{

    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse> GetTehsilList(int? Tehsilid)
        {
            return await _client.GetFromJsonAsync<ApiResponse>("api/tehsil-list?Tehsilid=" + Tehsilid);
        }

        public async Task<ApiResponse> CreateTehsil(TehsilViewModel data)
        {
            var response = await _client.PostAsJsonAsync("api/save-tehsil", data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }


    }

}
