using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.BusinessLogic
{
    public interface ITaskToDbInserter<T> where T : YoutubeItem
    {
        bool InsertAllElements(T element, int playListId);
    }
}
