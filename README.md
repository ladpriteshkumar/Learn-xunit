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
