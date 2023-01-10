using GuData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuData.DTO
{
    public class GroupDTO
    {
        public string GroupName { get; set; }
        public InstituteList Institute { get; set; }
    }
}
