using System;
using Xunit;
using TestClassLibrary;

namespace xunitTestProject
{
    public class UnitTest1
    {
        private readonly Calculator _calculator;

        public UnitTest1()
        {
            _calculator = new Calculator();
        }

        [Theory]
        [InlineData(5, 10, 15)]
        [InlineData(-3, 3, 0)]
        [InlineData(0, 0, 0)]
        public void Add_TwoNumbers_ReturnsSum(int a, int b, long expected)
        {
            var result = _calculator.Add(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_MultipleNumbers_ReturnsSum()
        {
            var result = _calculator.Add(1, 2, 3, 4, 5);
            Assert.Equal(15, result);
        }

        [Fact]
        public void Add_NullValues_ReturnsZero()
        {
            var result = _calculator.Add((int[])null);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Add_LargeValues_DoesNotOverflow()
        {
            var result = _calculator.Add(int.MaxValue, int.MaxValue);
            Assert.Equal((long)int.MaxValue + int.MaxValue, result);
        }

        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(0, 5, -5)]
        [InlineData(-5, -5, 0)]
        public void Subtract_TwoNumbers_ReturnsDifference(int a, int b, int expected)
        {
            var result = _calculator.Subtract(a, b);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(6, 7, 42)]
        [InlineData(0, 5, 0)]
        [InlineData(-3, 4, -12)]
        public void Multiply_TwoNumbers_ReturnsProduct(int a, int b, int expected)
        {
            var result = _calculator.Multiply(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Multiply_ByZero_ReturnsZero()
        {
            Assert.Equal(0, _calculator.Multiply(12345, 0));
        }

        [Theory]
        [InlineData(10, 3, 1)]
        [InlineData(9, 3, 0)]
        [InlineData(-7, 4, -7 % 4)]
        public void Mod_TwoNumbers_ReturnsRemainder(int a, int b, int expected)
        {
            var result = _calculator.Mod(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Mod_DivideByZero_Throws()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Mod(5, 0));
        }

        [Theory]
        [InlineData(10, 2, 5.0)]
        [InlineData(7, 2, 3.5)]
        [InlineData(-9, 3, -3.0)]
        public void Divide_TwoNumbers_ReturnsQuotient(int a, int b, double expected)
        {
            var result = _calculator.Divide(a, b);
            Assert.Equal(expected, result, 5);
        }

        [Fact]
        public void Divide_DivideByZero_Throws()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
        }

        [Fact]
        public void TryDivide_Success_ReturnsTrueAndResult()
        {
            var success = _calculator.TryDivide(7, 2, out double result);
            Assert.True(success);
            Assert.Equal(3.5, result, 5);
        }

        [Fact]
        public void TryDivide_DivideByZero_ReturnsFalseAndZeroResult()
        {
            var success = _calculator.TryDivide(7, 0, out double result);
            Assert.False(success);
            Assert.Equal(0, result, 5);
        }

        [Theory]
        [InlineData(2.0, 3.0, 8.0)]
        [InlineData(4.0, 0.5, 2.0)]
        [InlineData(-2.0, 3.0, -8.0)]
        public void Power_ValidInputs_ReturnsPower(double value, double exponent, double expected)
        {
            var result = _calculator.Power(value, exponent);
            Assert.Equal(expected, result, 5);
        }

        [Fact]
        public void Power_ZeroExponent_ReturnsOne()
        {
            Assert.Equal(1.0, _calculator.Power(5.0, 0.0), 5);
        }
    }
}
