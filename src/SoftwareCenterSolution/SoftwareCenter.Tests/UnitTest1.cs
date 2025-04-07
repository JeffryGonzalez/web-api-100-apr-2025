namespace SoftwareCenter.Tests;

public class UnitTest1
{
    [Fact]
    public void AddTwoAndTwo()
    {
        int a = 2, b = 2, answer;

        answer = a + b; // c# addition operator

        Assert.Equal( 4,answer);

    }

    [Fact]
    public void AddTenAndTwenty()
    {
        int a = 10, b = 20, answer;

        answer = a + b; // c# addition operator

        Assert.Equal(30, answer);
    }

    [Theory]
    [InlineData(2,2,4)]
    [InlineData(10,20,30)]
    [InlineData(100,8,108)]
    public void CanAddAnyInteger(int a, int b, int expected)
    {
        int answer = a + b;
        Assert.Equal(expected, answer);
    }
}
