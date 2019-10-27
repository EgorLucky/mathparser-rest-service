﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public static class MathParser
    {
        public static IFunction Parse(string mathExpression)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
            mathExpression = mathExpression.Replace(" ", "")
                                            .Replace("-", "+-1*")
                                            .Replace('.', ',');
                                            
            if (!Check.IsBracketsAreBalanced(mathExpression)) 
                throw new Exception("brackets are not balanced");
                
            mathExpression = mathExpression.ToLower();
            
            Sum result = ParseSum(mathExpression);
            
            return result;
        }
        
        static Sum ParseSum(string expression)
        {
            if (Check.IsExpressionInBrackets(expression)) 
                expression = expression.Remove(expression.Length - 1, 1)
                                        .Remove(0, 1);
            Sum result = new Sum();
            int balance = 0;
            string term = "";
            int counter = 0;
            
            foreach(var ch in expression)
            {
                counter++;
                if (ch == '(') 
                    balance--;
                if (ch == ')') 
                    balance++;
                if(ch == '+' && balance == 0)
                {
                    result.Terms.Add(ParseProduct(term)); 
                    term = "";
                    continue;
                }
                
                term += ch;
                if (counter == expression.Length)
                {
                    result.Terms.Add(ParseProduct(term));
                    continue;
                }
            }
            return result;
        }
        
        static Product ParseProduct(string expression)
        {
            if (Check.IsExpressionInBrackets(expression)) 
                expression = expression.Remove(expression.Length - 1, 1)
                                        .Remove(0, 1);
                                        
            Product result = new Product();
            int balance = 0;
            string factor = "";
            int counter = 0;
            
            foreach (var ch in expression)
            {
                counter++;
                if (ch == '(') 
                    balance--;
                if (ch == ')') 
                    balance++;
                    
                if (ch == '*' && balance == 0)
                {
                    result.Factors.Add(ParseFunction(factor)); 
                    factor = "";
                    continue;
                }
                
                factor += ch;
                if (counter == expression.Length)
                {
                    result.Factors.Add(ParseFunction(factor));
                    continue;
                }
            }
            return result;
        }
        
        static IFunction ParseFunction(string expression)
        {
            //поменять порядок действий (выражение exp1/exp1 распознается как exp(1/exp1))
            if (Check.IsExpressionPow(expression))
                return ParsePow(expression);
                
            if (Check.IsExpressionFraction(expression)) 
                return ParseFraction(expression);
                
            if (expression.StartsWith("sin"))                                   
                return ParseSin(expression);
                
            if (expression.StartsWith("cos"))
                return ParseCos(expression);
                
            if (expression.StartsWith("tg"))
                return ParseTg(expression);
                
            if (expression.StartsWith("ctg"))
                return ParseCtg(expression);
                
            if (expression.StartsWith("exp"))
                return ParseExp(expression);
                
            if (Check.IsExpressionInBrackets(expression))
                return ParseSum(expression);
                
            if (double.TryParse(expression, out double result))
                return ParseNumber(result);
                
            throw new Exception("Unknown function in expression: " + expression);
        }
        
        static IFunction ParseNumber(double value)
        {
            return new Number() { Value = value };
        }
        
        static IFunction ParseSin(string expression)
        {
            if(expression.StartsWith("sin"))
            {
                string argString = expression.Substring(3);
                Sin result = new Sin() { Argument = Parse(argString) };
                return result;
            }
            throw new Exception("This is not sin: " + expression);
        }
        
        static IFunction ParseCos(string expression)
        {
            if (expression.StartsWith("cos"))
            {
                string argString = expression.Substring(3);
                Cos result = new Cos() { Argument = Parse(argString) };
                return result;
            }
            throw new Exception("This is not cos: " + expression);
        }
        
        static IFunction ParseTg(string expression)
        {
            if (expression.StartsWith("tg"))
            {
                string argString = expression.Substring(2);
                Tg result = new Tg() { Argument = Parse(argString) };
                return result;
            }
            throw new Exception("This is not tg: " + expression);
        }
        
        static IFunction ParseCtg(string expression)
        {
            if (expression.StartsWith("ctg"))
            {
                string argString = expression.Substring(3);
                Ctg result = new Ctg() { Argument = Parse(argString) };
                return result;
            }
            throw new Exception("This is not ctg: " + expression);
        }
        
        static IFunction ParseExp(string expression)
        {
            if (expression.StartsWith("exp"))
            {
                string argString = expression.Substring(3);
                Exp result = new Exp() { Argument = Parse(argString) };
                return result;
            }
            throw new Exception("This is not exp: " + expression);
        }
        
        static IFunction ParsePow(string expression)
        {
            if (expression.Contains("^"))
            {
                int i = -1;
                foreach (var ch in expression)
                {
                    i++;
                    if (ch != '^') 
                        continue;
                    else if (i != 0 && i != expression.Length - 1)
                    {
                        var @base = expression.Substring(0, i);
                        var log = expression.Substring(i + 1);
                        if (Check.IsBracketsAreBalanced(@base) && Check.IsBracketsAreBalanced(log))
                            return new Pow() 
                            { 
                                Base = Parse(@base),
                                Log = Parse(log)
                            };
                    }
                }
            }
            throw new Exception("This is not pow: " + expression);
        }
        static IFunction ParseFraction(string expression)
        {
            if (expression.Contains("/"))
            {
                int i = -1;
                foreach (var ch in expression)
                {
                    i++;
                    if (ch != '/') 
                        continue;
                        
                    else if (i != 0 && i != expression.Length - 1)
                    {
                        var numerator = expression.Substring(0, i);
                        var denominator = expression.Substring(i + 1);
                        
                        if (Check.IsBracketsAreBalanced(numerator) && Check.IsBracketsAreBalanced(denominator))
                            return new Fraction() 
                            { 
                                Numerator = Parse(numerator), 
                                Denominator = Parse(denominator) 
                            };
                    }
                }
            }
            throw new Exception("This is not fraction: " + expression);
        }

        static class Check
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
            public static bool IsExpressionFraction(string expression)
            {
                bool result = false;
                int i = -1;
                foreach (var ch in expression)
                {
                    i++;
                    if (ch != '/') 
                        continue;
                    else if (i != 0 && i != expression.Length - 1)
                    {
                        if (IsBracketsAreBalanced(expression.Substring(0, i)) && IsBracketsAreBalanced(expression.Substring(i + 1)))
                        { 
                            result = true; 
                            return result; 
                        }
                    }
                }

                return result;
            }
            public static bool IsExpressionPow(string expression)
            {
                bool result = false;
                int i = -1;
                foreach(var ch in expression)
                {
                    i++;
                    if (ch != '^') 
                        continue;
                    else if (i != 0 && i != expression.Length - 1)
                    {
                        if (IsBracketsAreBalanced(expression.Substring(0, i)) && IsBracketsAreBalanced(expression.Substring(i + 1)))
                        { 
                            result = true; 
                            return result; 
                        }
                    }
                }
                return result;
            }
        }
    }
}
