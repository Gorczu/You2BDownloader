using SearchingModule.BusinessLogic;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.ViewModels
{
    public class YoutubeMovie : YoutubeItem
    {
        public override IList<YoutubeItem> GetAllElements()
        {
            return new YoutubeItem[] { this };
        }
    }
}
