using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuData.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupNumber { get; set; }

        public InstituteList Institute { get; set; }
    }
}
