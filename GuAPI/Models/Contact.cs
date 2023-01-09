using System.ComponentModel.DataAnnotations;

namespace GuAPI.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }
        public int GroupId { get; set; }
    }
}
