using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestIARA.Application.Common.Interfaces;
using TestIARA.Application.Common.Models;

namespace TestIARA.Infrastructure.Services
{
    public class CEPService: ICEPService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly HttpClient _httpClient;

        public CEPService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _httpClient = new HttpClient(httpClientHandler);
        }

        public CEPDto SearchCEP(string CEP)
        {
            if (String.IsNullOrEmpty(CEP)) return null;

            var errorObject = new CEPDto() {
                logradouro = "-",
                bairro = "-",
                uf = "-"
            };
            try
            {
                _httpClient.BaseAddress = new Uri("https://viacep.com.br/");
                string URL = "ws/" + CEP + "/json";
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                // _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_httpContext.HttpContext.Request.Headers["Authorization"]);

                HttpResponseMessage response = _httpClient.GetAsync(URL).Result;
                if (response.IsSuccessStatusCode)
                {
                    String responseStr = response.Content.ReadAsStringAsync().Result;
                    CEPDto cepObject = JsonConvert.DeserializeObject<CEPDto>(responseStr);
                    if (cepObject.erro)
                        cepObject = errorObject;
                    return cepObject;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return errorObject;
        }
    }
}
