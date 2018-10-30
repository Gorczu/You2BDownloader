using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.ViewModels
{
    public interface ISearchingViewModel
    {
        IList<YoutubeItem> Result { get; set; }
    }
}
