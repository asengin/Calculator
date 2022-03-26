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

        #region Свойство отвечающее за взаимодействие с дисплеем с формулой
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

        public MainWindowsViewModel() //Конструктор с начальной инициализацией
        {
            displayExpression = string.Empty;
            displayResult = "0";

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
            //DisplayResult = Convert.ToDouble(DisplayResult.ToString() + parameter.ToString()); //Выполнение конкатенации вводимых чисел, для отображения на дисплее ввода
        }
        #endregion

        #region Команда нажатия кнопок действий
        public ICommand PressOperationButton { get; }
        private void OnPressOperationButon(object parameter)
        {
            //string strP = parameter.ToString(); //Перевод параметра в строку
            //if (strP == "-" || strP == "+" || strP == "*" || strP == "/" || strP == "^2" || strP == "(" || strP == ")")
            //{
            //    DisplayResult = 0;
            //    DisplayExpression += strP;
            //}
            //else if (strP == "sqrt")
            //{
            //    DisplayResult = 0;
            //    DisplayExpression += strP + "(";
            //}
            //else if (strP == "negative")
            //{
            //    DisplayResult *= -1;
            //    DisplayExpression += DisplayResult.ToString(); //Доработать
            //}
            //else if (strP == "C")
            //{
            //    DisplayResult = 0;
            //    DisplayExpression = string.Empty;
            //}
            //else if (strP == ".")
            //{

            //}
            //else if (strP == "CE")
            //{
            //    if (DisplayResult != 0 && DisplayResult.ToString().Length != 1) //Доработать
            //    {
            //        DisplayResult = Convert.ToDouble(DisplayResult.ToString().Remove(DisplayResult.ToString().Length - 1, 1));
            //        DisplayExpression = DisplayExpression.Remove(DisplayExpression.Length - 1, 1); //Доработать
            //    }
            //}

            //DisplayExpression += parameter.ToString();

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
                        }
                        break;
                    }
            }
        }
        #endregion
    }

}
