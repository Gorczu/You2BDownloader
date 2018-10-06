using CommonControls.Validation;
using Prism.Mvvm;
using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonControls.VM
{
    public class ListItemViewModel : ValidatableBindableBase
    {
        public ListItemViewModel()
        {
            this.PropertyChanged += (a, b) => this.ValidateProperties();
        }

        private int _id;
        private string _name;
        private string _gerne;
        private DateTime _created;
        private string _description;
        private string _path;
        private byte[] _image = File.ReadAllBytes("ResourcesCommonControls/stormtrooper.png");
        private Object _objectToInsert; 

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id , value);
        }

        [Required(ErrorMessage ="Name is required.")]
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                RaisePropertyChanged("NameErrorVisibility");
            }
        }

        public string Gerne
        {
            get => _gerne;
            set => SetProperty(ref _gerne, value);
        }

        public DateTime Created
        {
            get => _created;
            set => SetProperty(ref _created, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        [NewItemPathValidation(ErrorMessage ="You have to precise path for playlist.")]
        public string Path
        {
            get => _path;
            set
            {
                SetProperty(ref _path, value);
                RaisePropertyChanged("PathErrorVisibility");
            }
        }

        public byte[] Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public Visibility NameErrorVisibility
        {
            get
            {
                if (Errors.Errors.TryGetValue("Name", out ReadOnlyCollection<string> res))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility PathErrorVisibility
        {
            get
            {
                if (Errors.Errors.TryGetValue("Path", out ReadOnlyCollection<string> res))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public object ObjectToInsert
        {
            get => _objectToInsert;
            set => _objectToInsert = value;
        }
    }
}
