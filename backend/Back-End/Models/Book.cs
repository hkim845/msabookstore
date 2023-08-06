using System.Text.Json.Serialization;

namespace Back_End.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string GoogleId { get; set; }
        public string SelfLink { get; set; }


    }
}
