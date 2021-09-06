using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductDetails
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string GTIN { get; set; }
        public bool IsOnDiscount { get; set; }
        public bool IsDomestic { get; set; }
        public int CountryId { get; set; }
        public Country CountryOfOrigin { get; set; }

    }
}