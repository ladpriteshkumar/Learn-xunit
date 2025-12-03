using System;
using System.Collections.Generic;

namespace TestClassLibrary
{
    public class Calculator
    {

        public long Add(params int[] values)
        {
            long sum = 0;
            if (values == null)
            {
                return 0;
            }

            foreach (var v in values)
            {
                sum += v;
            }

            return sum;
        }

        public int Subtract(int a, int b) => a - b;

        public int Multiply(int a, int b) => a * b;

        public int Mod(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Modulus by zero.");
            }

            return a % b;
        }

        public double Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Division by zero.");
            }

            return (double)a / b;
        }

        public bool TryDivide(int a, int b, out double result)
        {
            if (b == 0)
            {
                result = 0;
                return false;
            }

            result = (double)a / b;
            return true;
        }

        public double Power(double value, double exponent) => Math.Pow(value, exponent);
    }
}
