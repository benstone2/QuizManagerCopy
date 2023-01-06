using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Web.Models.Editor
{
    public class AnswerNumber
    {
        public int Number { get; set; }

        public string Letter => GetLetter();

        public AnswerNumber(int number)
        {
            Number = number;
        }

        private string GetLetter()
        {
            switch (Number)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                default:
                    return "OutOfRange";
            }
        }
    }
}
