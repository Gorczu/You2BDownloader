using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Respositories
{
    public class PlaylistItemRepository : IRepository<PlaylistItem>
    {
        public bool DeleteItem(int idx)
        {
            throw new NotImplementedException();
        }

        public bool EditItem(int idx, PlaylistItem newValue)
        {
            throw new NotImplementedException();
        }

        public PlaylistItem GetItem(int idx)
        {
            throw new NotImplementedException();
        }

        public IList<PlaylistItem> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            throw new NotImplementedException();
        }

        public bool InsertItem(PlaylistItem item)
        {
            throw new NotImplementedException();
        }
    }
}
