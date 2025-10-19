using BasetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasetApi.Config
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
                new Comment { CommentID=1, CommentUserName="Ceyhun", CommentUserSurname="Yusif",CommentUserComment="Çok iyi", CommentUserMail="jeyhun312@gmail.com",  CommentStatus=true, ProductID=1, UserID=null },
                new Comment { CommentID=2, CommentUserName="Test", CommentUserSurname="Test2",CommentUserComment="Çok Beğemdim", CommentUserMail="Test22@gmail.com", CommentStatus=true,ProductID=2,UserID=null},
                new Comment { CommentID=3, CommentUserName="Test2", CommentUserSurname="Test3", CommentUserComment="Beğenmedim",CommentUserMail="Test22@gmail.com",  CommentStatus=true,ProductID=2,UserID=null}
            );
        }
    }
} 