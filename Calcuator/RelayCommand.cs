using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calcuator
{
    class RelayCommand : ICommand

    {
        #region Поля
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        #endregion

        #region Реализация интерфейса ICommand
        public event EventHandler CanExecuteChanged //Передача управления CommandManager-у
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute = null) //Конструктор, принимающий 2 делегата, соответствующих методам Execute и CanExecute
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(Execute)); // присваивается, если не null. усли null, то исключение
            canExecute = CanExecute;
        }

        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true; //если не null, то Invoke. Если null, то вернет true

        public void Execute(object parameter) => execute(parameter);
        
        #endregion
    }
}
