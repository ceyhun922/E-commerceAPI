using System.ComponentModel.DataAnnotations;

namespace BasetApi.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public string? CommentUserName { get; set; }
        public string? CommentUserSurname { get; set; }
        public string? CommentUserMail { get; set; }
        public string? CommentUserComment { get; set; }
        public bool CommentStatus { get; set; }
        public int ProductID { get; set; }

        public string? UserID { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}