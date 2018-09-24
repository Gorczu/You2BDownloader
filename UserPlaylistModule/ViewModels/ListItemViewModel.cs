using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchModule.ViewModels
{
    public class ListItemViewModel : BindableBase
    {
        private string _name;
        private string _gerne;
        private string _created;
        private string _description;
        private string _path;

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Gerne { get => _gerne; set => SetProperty(ref _gerne, value); }
        public string Created { get => _created; set => SetProperty(ref _created, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public string Path { get => _path; set => SetProperty(ref _path , value); }
    }
}
