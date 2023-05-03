using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Foods")]
    public class Food
    {
        [Key]  //主索引鍵
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //電腦自動編號
        public int FoodId { get; set; }

        [StringLength(64)]  //儲存格式，括號未設定預設為Max (不推薦)
        public string? Title { get; set; }


        [StringLength(13)]
        [Column(TypeName = "nchar")]
        public string? ISBN { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }        

    }
}
