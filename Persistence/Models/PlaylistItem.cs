using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class PlaylistItem
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string NewName { get; set; }
        public string Desrciption { get; set; }
    }
}
