using Calcuator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        #region Свойство отвечающее за взаимодействие с дисплеем с выражением
        private string displayExpression;

        public string DisplayExpression
        {
            get => displayExpression;
            set
            {
                displayExpression = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Свойство, отвечающее за взаимодействие с дисплеем результата
        private string displayResult;

        public string DisplayResult
        {
            get { return displayResult; }
            set { displayResult = value; OnPropertyChanged(); }
        }
        #endregion

        #region Свойство, отвечающее за взаимодействие с историей вычислений
        private string historyResult;

        public string HistoryResult
        {
            get { return historyResult; }
            set { historyResult = value; OnPropertyChanged(); }
        }

        #endregion

        public MainWindowsViewModel() //Конструктор с начальной инициализацией
        {
            displayExpression = string.Empty;
            displayResult = "0";
            historyResult = string.Empty;

            PressCalcButton = new RelayCommand(OnPressCalcButtonExecute);
            PressOperationButton = new RelayCommand(OnPressOperationButon);
        }

        #region Команда нажатия кнопки цифр и операций
        public ICommand PressCalcButton { get; }
        private void OnPressCalcButtonExecute(object parameter)
        {
            if (DisplayExpression.EndsWith("=")) //Если в дисплее варажения последний символ "=", значит результат переносим в выражение и обнуляем результат.
            {
                DisplayExpression = DisplayResult;
                DisplayResult = "0";
            }
            DisplayExpression += parameter.ToString();
            
        }
        #endregion

        #region Команда нажатия кнопок действий
        public ICommand PressOperationButton { get; }
        private void OnPressOperationButon(object parameter)
        {
            switch (parameter)
            {
                case "negative":
                    {
                        break;
                    }
                case "back":
                    {
                        DisplayExpression = DisplayExpression.Length > 1 ? DisplayExpression.Remove(DisplayExpression.Length - 1) : string.Empty;
                        break;
                    }
                case "C":
                    {
                        DisplayExpression = string.Empty;
                        DisplayResult = "0";
                        break;
                    }
                case "=":
                    {
                        if (string.IsNullOrEmpty(DisplayExpression)) //Проверяем есть ли выражение для вычисления
                        {
                            DisplayResult = "Нет выражения";
                        }
                        else
                        {
                            DisplayResult = Calculation.Result(DisplayExpression);
                            DisplayExpression += parameter.ToString();
                            HistoryResult+=(DisplayExpression + DisplayResult+"\n");
                        }
                        break;
                    }
            }
        }
        #endregion
    }

}
