using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yemekler
{
    public class ApiService
    {
        private readonly string apiUrl = "https://api.spoonacular.com/recipes/complexSearch";
        private readonly string apiKey = "da524e39a6004eaf822f64361333a579"; // Spoonacular API anahtarınızı buraya ekleyin

        public async Task<string> GetRecipesAsync(string query = "", int number = 10)
        {
            using (HttpClient client = new HttpClient())
            {
                // API URL'sine istek gönder
                string requestUrl = $"{apiUrl}?apiKey={apiKey}&query={query}&number={number}";
                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync(); // JSON formatında dönen veriyi al
                }
                else
                {
                    throw new Exception("API çağrısı başarısız! Hata: " + response.StatusCode);
                }
            }
        }
    }
}