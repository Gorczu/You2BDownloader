﻿using CommonControls.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchModule.ViewModels
{
    public interface IUserPlaylistViewModel
    {
        ListItemViewModel CurrenItem { get; set; }
        List<ListItemViewModel> PlaylistCollection { get; set; }
    }
}
