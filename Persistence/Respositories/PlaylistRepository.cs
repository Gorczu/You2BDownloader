using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Persistence.Respositories
{
    public class PlaylistRepository : IRepository<PlayList>
    {
        public bool DeleteItem(int idx)
        {
            throw new NotImplementedException();
        }

        public bool EditItem(int idx, PlayList newValue)
        {
            throw new NotImplementedException();
        }

        public PlayList GetItem(int idx)
        {
            throw new NotImplementedException();
        }

        public IList<PlayList> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            throw new NotImplementedException();
        }

        public bool InsertItem(PlayList item)
        {
            throw new NotImplementedException();
        }
    }
}
