using sl.domain.Exceptions;
using sl.domain.Models;
using Xunit;

namespace sl.domain.unitTests
{
    public class LabelTest
    {
        [Fact]
        public void Label_Equals()
        {
            // arrange
            var label = "by Diyaz";

            // act
            var labelInstance1 = new Label(label);
            var labelInstance2 = new Label(label);

            // assert
            Assert.Equal(labelInstance1, labelInstance2);
        }

        [Theory]
        [InlineData("custom class")]
        [InlineData("grid")]
        public void Label_Create(string label)
        {
            // act
            var newLabel = new Label(label);

            // assert
            Assert.NotNull(newLabel);
            Assert.Equal(label, newLabel.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Label_Invalid_Create(string label)
        {
            // arrange
            Label newLabel = null;

            // act n assert
            Assert.Throws<LabelDomainException>(() => new Label(label));
            Assert.Null(newLabel);
        }
    }
}
