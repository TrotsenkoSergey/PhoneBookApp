using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class PhoneBook
    {
        public int Id { get; set; }

        [Required] // not null
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required] 
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        
        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
