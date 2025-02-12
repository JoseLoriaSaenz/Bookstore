using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        [MaxLength(17)]
        public string ISBN { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Genre {  get; set; }
    }
}
