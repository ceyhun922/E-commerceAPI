using System.ComponentModel.DataAnnotations;

namespace BasetApi.Models
{
    public class Option
    {
        [Key]
        public int OptionID { get; set; }
        public int ProductID { get; set; }
        public string? Duration { get; set; }
        public Product? Product { get; set; }
    }
}