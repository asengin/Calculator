using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;

namespace Calcuator.Models
{
    class Calculation
    {
        public static string Result(string expression)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); //Подключение западной локали, чтобы результат double возвращался с "." вместо ","
            Expression result = new Expression(expression);
            return result.calculate().ToString();
        }

    }
}
