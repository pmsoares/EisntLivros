using System.ComponentModel.DataAnnotations;

namespace EisntLivros.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        public string ISBN { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        public string ImageURL { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [Required]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; } = null!;
    }
}
