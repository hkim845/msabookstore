using Back_End.Contexts;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Back_End.Services
{
    public class BookService
    {
        private BookContext _bookContext;
        private HttpClient _httpClient;

        public BookService(BookContext context)
        {
            _bookContext = context;
            _httpClient = new HttpClient();
        }

        public async Task<string> SearchBooks()
        {
            string apiKey = "AIzaSyALdC7SfTRXJ_zV5KCsnwyiDqVoBGXwU2Q";
            string url = $"https://www.googleapis.com/books/v1/volumes?q=flowers+inauthor:keyes&key={apiKey}";

            try
            {

                // Send the GET request and wait for the response
                var response = await _httpClient.GetAsync(url);

                // Check if the response is successful (status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic books = JsonConvert.DeserializeObject<dynamic>(responseContent);

                    return JsonConvert.SerializeObject(books.items);
                }
                else
                {
                    return ($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                return $"HTTP Request Error: {ex.Message}";
            }        
        }
    }
}
