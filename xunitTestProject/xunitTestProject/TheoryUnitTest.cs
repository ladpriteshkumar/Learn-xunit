using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Xunit;
using TestClassLibrary;
using Xunit.Sdk;

namespace xunitTestProject
{
    public class TheoryUnitTest
    {
        private readonly Calculator _calculator;

        public TheoryUnitTest()
        {
            _calculator = new Calculator();
        }

        // -----------------------
        // InlineData examples
        // -----------------------

        [Theory]
        [InlineData(5, 10, 15L)]
        [InlineData(-3, 3, 0L)]
        [InlineData(0, 0, 0L)]
        public void Add_TwoNumbers_ReturnsSum(int a, int b, long expected)
        {
            var result = _calculator.Add(a, b);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 3, 4, 5, 15L)]
        [InlineData(-1, -2, 3, 0, 0, 0L)]
        public void Add_FiveNumbers_ReturnsSum(int a, int b, int c, int d, int e, long expected)
        {
            var result = _calculator.Add(a, b, c, d, e);
            Assert.Equal(expected, result);
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

        [Theory]
        [InlineData(10, 3, 1, false)]
        [InlineData(9, 3, 0, false)]
        [InlineData(5, 0, 0, true)]
        public void Mod_TwoNumbers_Behavior(int a, int b, int expected, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<DivideByZeroException>(() => _calculator.Mod(a, b));
            }
            else
            {
                var result = _calculator.Mod(a, b);
                Assert.Equal(expected, result);
            }
        }

        [Theory]
        [InlineData(10, 2, 5.0, false)]
        [InlineData(7, 2, 3.5, false)]
        [InlineData(10, 0, 0.0, true)]
        public void Divide_TwoNumbers_Behavior(int a, int b, double expected, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
            }
            else
            {
                var result = _calculator.Divide(a, b);
                Assert.Equal(expected, result, 5);
            }
        }

        [Theory]
        [InlineData(7, 2, true, 3.5)]
        [InlineData(7, 0, false, 0.0)]
        public void TryDivide_TwoNumbers_ReturnsSuccessFlagAndResult(int a, int b, bool expectedSuccess, double expectedResult)
        {
            var success = _calculator.TryDivide(a, b, out double result);
            Assert.Equal(expectedSuccess, success);
            Assert.Equal(expectedResult, result, 5);
        }

        [Theory]
        [InlineData(2.0, 3.0, 8.0)]
        [InlineData(4.0, 0.5, 2.0)]
        [InlineData(-2.0, 3.0, -8.0)]
        [InlineData(5.0, 0.0, 1.0)]
        public void Power_ValidInputs_ReturnsPower(double value, double exponent, double expected)
        {
            var result = _calculator.Power(value, exponent);
            Assert.Equal(expected, result, 5);
        }

        // -----------------------
        // MemberData example (for params / null)
        // -----------------------
        public static IEnumerable<object[]> AddParamsMemberData()
        {
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 }, 15L };
            yield return new object[] { new int[] { int.MaxValue, int.MaxValue }, (long)int.MaxValue + int.MaxValue };
            yield return new object[] { null, 0L }; // null should return 0 per implementation
            yield return new object[] { new int[] { -1, -2, 3 }, 0L };
        }

        [Theory]
        [MemberData(nameof(AddParamsMemberData))]
        public void Add_Params_MemberData_ReturnsSum(int[] values, long expected)
        {
            var result = _calculator.Add(values);
            Assert.Equal(expected, result);
        }

        // -----------------------
        // ClassData example
        // -----------------------
        public class AddPairsClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 2, 3, 5L };
                yield return new object[] { -2, -4, -6L };
                yield return new object[] { 0, 0, 0L };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(AddPairsClassData))]
        public void Add_WithClassData_ReturnsSum(int a, int b, long expected)
        {
            var result = _calculator.Add(a, b);
            Assert.Equal(expected, result);
        }

        // -----------------------
        // Custom DataAttribute example
        // -----------------------
        public class DivideDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            {
                yield return new object[] { 10, 2, 5.0, false };
                yield return new object[] { 7, 2, 3.5, false };
                yield return new object[] { 10, 0, 0.0, true };
            }
        }

        [Theory]
        [DivideData]
        public void Divide_CustomDataBehavior(int a, int b, double expected, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
            }
            else
            {
                var result = _calculator.Divide(a, b);
                Assert.Equal(expected, result, 5);
            }
        }
    }

    public class TheoryUnitTest_ExternalData
    {
        private readonly Calculator _calculator = new Calculator();

        public static IEnumerable<object[]> LoadAddCasesFromJson()
        {
            var json = File.ReadAllText("TestData/add_cases.json"); // keep test data under repo (not committed secrets)
            var cases = JsonSerializer.Deserialize<List<AddCase>>(json);
            foreach (var c in cases) yield return new object[] { c.A, c.B, (long)c.Expected };
        }

        [Theory]
        [MemberData(nameof(LoadAddCasesFromJson))]
        public void Add_FromJson_ReturnsExpected(int a, int b, long expected)
        {
            Assert.Equal(expected, _calculator.Add(a, b));
        }

        private class AddCase { public int A { get; set; } public int B { get; set; } public long Expected { get; set; } }
    }
}
