using System.Text.Json;
using System.Text.Json.Serialization;

namespace PhoneBookApp.WebSite.Models
{
    public class PhoneBook
    {
        public int Id { get; set; }
        
        public string LastName { get; set; }

        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
