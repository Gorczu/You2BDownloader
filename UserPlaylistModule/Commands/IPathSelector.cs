﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UserPlaylistModule.Commands
{
    public interface IPathSelector : ICommand
    {
        void SetPath(Action<string> setPathAction);
    }
}