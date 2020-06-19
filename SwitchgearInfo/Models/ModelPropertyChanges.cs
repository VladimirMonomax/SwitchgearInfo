using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SwitchgearInfo.Models
{
    public class ModelPropertyChanges : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
