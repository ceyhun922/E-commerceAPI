using BasetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasetApi.Config
{
    public class OptionConfig : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasData(
                new Option { OptionID=1, Duration= "1 Ay -10 Azn", ProductID=1},
                new Option { OptionID=2, Duration= "2 Ay -20 Azn",ProductID=1},
                new Option { OptionID=3, Duration= "3 Ay -40 Azn",ProductID=1}
            );
        }
    }
}