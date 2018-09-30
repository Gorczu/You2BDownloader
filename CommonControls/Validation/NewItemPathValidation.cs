using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControls.Validation
{
    public class NewItemPathValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var vm = (string)value;
         
            // The [Required] attribute should test this.
            if (value == null && vm == null)
            {
                this.ErrorMessage = "You have to specify path where playlist will be saved.";
                return false;
            }
            else
            {
                if(!Directory.Exists(vm))
                {
                    this.ErrorMessage = "Specified path doesn't exist.";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }
    }
}
