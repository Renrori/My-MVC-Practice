using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstTest.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }

        [ForeignKey("Foods")]
        public int FoodId { get; set; }

        public string? ReviewText { get; set; }

        public virtual Food? Food { get; set; }
    }
}