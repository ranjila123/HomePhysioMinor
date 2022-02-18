using HomePhysio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomePhysio.Services.Payment
{
    public class PaymentService:IPaymentService
    {
        private HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }
        public async Task<KhaltiResponseVM> KhaltiPay(PayloadPostVM payloadPostVM)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Key test_secret_key_8590843a9fcf49289f70c37b068d8e0f");
            var response = await _httpClient.PostAsJsonAsync("https://khalti.com/api/v2/payment/verify/", payloadPostVM);
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<KhaltiResponseVM>(responseBody);
        }
    }
}
