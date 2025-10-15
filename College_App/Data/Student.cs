using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

     
        public string Name { get; set; } = string.Empty;

       
        public string Email { get; set; } = string.Empty;

      

        public string Address { get; set; } = string.Empty;

     
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
