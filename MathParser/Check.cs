using System.Linq;

namespace MathParserClasses
{
    public static class Check
    {
        public static bool IsExpressionInBrackets(string expression)
        {
            if (expression.First() != '(' || expression.Last() != ')')
                return false;

            int balance = -1;
            for (int i = 1; i < expression.Length; i++)
            {
                if (expression[i] == ')')
                    balance++;
                else if (expression[i] == '(')
                    balance--;

                if (balance == 0 && (i + 1) != expression.Length)
                    return false;
            }

            return balance == 0;
        }
        public static bool IsBracketsAreBalanced(string expression)
        {
            int balance = 0;
            foreach (var c in expression)
            {
                if (c == '(')
                    balance--;
                else if (c == ')')
                    balance++;
            }
            return balance == 0;
        }
    }
}
