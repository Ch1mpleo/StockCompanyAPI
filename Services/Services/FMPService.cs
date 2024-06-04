using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Porfolio_API.Models;
using Repository.DTOs.Stock;
using Repository.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    //Seed data using FMP (Financial Modeling Prep)
    public class FMPService : IFMPService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;

        public FMPService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");
                
                //Validata lại result xem đã lấy được data chưa
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var task = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = task[0];
                    if (stock != null)
                    {
                        return stock.ToStockFromFMP();
                    }
                    return null;
                }
                return null;

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
