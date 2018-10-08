using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.BusinessLogic
{
    public class PlaylistToDbInserter<T> : ITaskToDbInserter<T> where T : YoutubeItem
    {
        public bool InsertAllElements(T element)
        {
            throw new NotImplementedException();
        }
    }
}
