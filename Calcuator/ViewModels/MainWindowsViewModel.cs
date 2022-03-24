using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calcuator.ViewModels
{
    class MainWindowsViewModel : INotifyPropertyChanged
    {
        #region Событие-уведомление об изменении и метод вызова Invoke
        public event PropertyChangedEventHandler PropertyChanged; //Реализация интерфейса INotifyPropertyChanged

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName)); //Запуск методов, переданных с PropertyChanged
        }
        #endregion

        private string displayFormula;

        public string DisplayFormula
        {
            get => displayFormula;
            set
            {
                displayFormula = value;
                OnPropertyChanged();
            }
        }


    }

}
