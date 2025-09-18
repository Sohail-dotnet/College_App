using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).UseIdentityColumn();

            builder.Property(n => n.Name).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Email).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.PhoneNumber).HasMaxLength(15);

            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    Name = "Sohail",
                    Email = "sohailshaik1026@gmail.com",
                    Address = "Bengaluru",
                    PhoneNumber = "6301310871"
                },
                new Student
                {
                    Id = 2,
                    Name = "Abhimanyu",
                    Email = "abimanyu@gmail.com",
                    Address = "Bengaluru",
                    PhoneNumber = "7452103698"
                }
            });
        }
    }
}
