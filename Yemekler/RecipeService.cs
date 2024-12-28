using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Yemekler
{
    public class RecipeService
    {
        private const string apiKey = "da524e39a6004eaf822f64361333a579";
        private const string baseUrl = "https://api.spoonacular.com/recipes/complexSearch";
        private readonly string connectionString = "Server=localhost;Database=Rec;User Id=root;Password=1234;";

        private static readonly HttpClient client = new HttpClient();

        public List<string> GetCategories()
        {
            return new List<string> { "main course", "dessert", "appetizer", "salad", "breakfast", "soup", "drink" };
        }


        public async Task FetchAndSaveRecipesByCategoryAsync(string category)
        {
            string url = $"{baseUrl}?type={category}&number=10&apiKey={apiKey}&addRecipeInformation=true&fillIngredients=true";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Header bilgilerini kontrol et
                Console.WriteLine("API Headers:");
                foreach (var header in response.Headers)
                {
                    Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
                }

                // API yanıtını işle
                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                foreach (var recipe in data["results"])
                {
                    string yemekAdi = recipe["title"]?.ToString() ?? "Bilinmeyen Tarif";
                    string resimUrl = recipe["image"]?.ToString() ?? "default_image_url";
                    string hazirlikSuresi = recipe["readyInMinutes"]?.ToString() ?? "0";
                    string tarifMetni = recipe["instructions"]?.ToString() ?? "Tarif mevcut değil.";
                    string malzemeler = GetIngredients(recipe);

                    InsertIntoDatabase(yemekAdi, resimUrl, hazirlikSuresi, category, tarifMetni, malzemeler);
                }
            }
            else
            {
                // Başarısız durumlarda hata mesajını yazdır
                Console.WriteLine($"API Hatası: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }


        private string GetIngredients(JToken recipe)
        {
            var ingredients = recipe["extendedIngredients"];
            if (ingredients == null) return "Malzeme bilgisi yok.";

            List<string> ingredientList = new List<string>();
            foreach (var item in ingredients)
            {
                ingredientList.Add(item["original"].ToString());
            }
            return string.Join(", ", ingredientList);
        }

        private void InsertIntoDatabase(string yemekAdi, string resimUrl, string hazirlikSuresi, string kategori, string tarifMetni, string malzemeler)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Çift kayıt kontrolü
                    string checkQuery = "SELECT COUNT(*) FROM tarifler WHERE YemekAdi = @YemekAdi AND Kategori = @Kategori";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@YemekAdi", yemekAdi);
                        checkCmd.Parameters.AddWithValue("@Kategori", kategori);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            Console.WriteLine("Bu tarif zaten kayıtlı.");
                            return;
                        }
                    }

                    // Veri ekleme
                    string query = "INSERT INTO tarifler (YemekAdi, ResimUrl, HazirlikSuresi, Kategori, TarifMetni, Malzemeler) " +
                                   "VALUES (@YemekAdi, @ResimUrl, @HazirlikSuresi, @Kategori, @TarifMetni, @Malzemeler)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@YemekAdi", yemekAdi);
                        cmd.Parameters.AddWithValue("@ResimUrl", resimUrl);
                        cmd.Parameters.AddWithValue("@HazirlikSuresi", hazirlikSuresi ?? "Bilinmiyor");
                        cmd.Parameters.AddWithValue("@Kategori", kategori);
                        cmd.Parameters.AddWithValue("@TarifMetni", tarifMetni ?? "Tarif bilgisi mevcut değil.");
                        cmd.Parameters.AddWithValue("@Malzemeler", malzemeler ?? "Malzeme bilgisi mevcut değil.");

                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Tarif '{yemekAdi}' başarıyla kaydedildi.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veritabanı hatası: {ex.Message}");
            }
        }



        public List<dynamic> GetRecipesByName(string query)
        {
            List<dynamic> recipes = new List<dynamic>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Benzersiz sonuçları döndüren SQL sorgusu
                string sql = @"
                SELECT YemekAdi, ResimUrl, MIN(Id) AS Id 
                FROM tarifler 
                WHERE YemekAdi LIKE @Query 
                GROUP BY YemekAdi, ResimUrl";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Query", $"%{query}%");
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recipes.Add(new
                            {
                                YemekAdi = reader["YemekAdi"].ToString(),
                                ResimUrl = reader["ResimUrl"].ToString(),
                                Id = Convert.ToInt32(reader["Id"])
                            });
                        }
                    }
                }
            }

            return recipes;
        }



    }
}
