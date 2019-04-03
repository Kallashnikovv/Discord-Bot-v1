using Xunit;

namespace DiscordBotV1.xUnit.Tests
{
    public class UtilityTests
    {
        [Fact]
        public void MyFirstTest()
        {
            const int expected = 5;

            var actual = Utilities.MyUtility(expected);

            Assert.Equal(expected, actual);
        }
    } 
}
