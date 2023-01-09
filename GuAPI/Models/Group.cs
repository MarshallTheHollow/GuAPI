using System.ComponentModel.DataAnnotations;

namespace GuAPI.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupNumber { get; set; }

        public InstituteList Institute { get; set; }
    }
}
