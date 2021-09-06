using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class SubCategory : BaseEntity
    {
        [Required]
       public string Name { get; set; }
       public int? CategoryId { get; set; }
       public Category Category { get; set; }
    }
}