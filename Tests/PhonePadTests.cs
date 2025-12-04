using Xunit;

namespace CodingChallenge.Tests
{
    public class PhonePadTests
    {
        [Theory]
        [InlineData("33#", "E")]
        [InlineData("227*#", "B")]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("8 88777444666*664#", "TURING")]
        public void TestExamples(string input, string expected)
        {
            var result = PhonePad.OldPhonePad(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("#", "")]
        [InlineData("2#", "A")]
        [InlineData("222#", "C")]          // triple press same key
        [InlineData("2222#", "A")]         // wrap-around
        [InlineData("7777#", "S")]         // 4 presses on key 7
        [InlineData("9999#", "Z")]         // 4 presses on key 9
        public void TestBasicPresses(string input, string expected)
        {
            var result = PhonePad.OldPhonePad(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2 2 2#", "AAA")]       // spaces break sequences
        [InlineData("222 2#", "CA")]
        [InlineData("22*2#", "B")]          // delete before finishing
        [InlineData("226662#", "BO")]       // multi groups
        public void TestSpacingAndDelete(string input, string expected)
        {
            var result = PhonePad.OldPhonePad(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2*#", "")]             // delete removes A
        [InlineData("23*#", "A")]           // delete last -> keep first
        [InlineData("233*3#", "D")]         // after delete new group starts
        public void TestDeleteCases(string input, string expected)
        {
            var result = PhonePad.OldPhonePad(input);
            Assert.Equal(expected, result);
        }
    }
}
