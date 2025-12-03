# Learn-xunit

### Test Methods   
- [Fact]
  
  Used for non-parameterized, always-true tests.

- [Theory]

  Used for parameterized, data-driven tests.

### Data Sources for Theories
   
To supply data to [Theory] methods, xUnit provides additional attributes:
- [InlineData]: Passes simple values directly.
- [MemberData]: Uses static members/properties of a class as data sources.
- [ClassData]: Uses a class that implements IEnumerable<object[]> as a data source.


### Skipping Tests
Both [Fact] and [Theory] allow you to skip tests with the Skip property:
```csharp
[Fact(Skip = "Reason for skipping")]
public void ThisTestIsSkipped() { ... }
```


### how do you validate data in xUnit or nUnit
In both xUnit and NUnit, you validate data using assertions. Assertions check whether the actual output of your code matches the expected result. In xUnit, you use the Assert class (e.g., Assert.Equal, Assert.True),

