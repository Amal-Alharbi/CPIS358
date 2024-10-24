using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        [Display(Name = "Number of Pages")]
        public double Pages { get; set; }
        public double Price { get; set; }
        public string photo { get; set; }
    }
}
