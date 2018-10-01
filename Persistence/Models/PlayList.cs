using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class PlayList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<PlaylistItem> Items { get; set; }
        public string FolderPath { get; set; }
        public string Description { get; set; }
        public string Gerne { get; set; }
        public DateTime StartGeneration { get; set; }
        public byte[] Image { get; set; }
    }
}
