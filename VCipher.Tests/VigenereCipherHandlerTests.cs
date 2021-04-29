using System;
using Xunit;

namespace VCipher.Tests
{
    public class VigenereCipherHandlerTests
    {
        [Fact]
        public void VigenereCipherHandlerEncode()
        {
            // Arrange
            VigenereCipherHandler vigenereCipherHandler = new VigenereCipherHandler("привет", "пока");
            // Act
            string result = vigenereCipherHandler.EncodeText();
            // Assert
            Assert.Equal("яяувфб", result);
        }

        [Fact]
        public void VigenereCipherHandlerDecode()
        {
            // Arrange
            VigenereCipherHandler vigenereCipherHandler = new VigenereCipherHandler("бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!", "скорпион");
            // Act
            string result = vigenereCipherHandler.DecodeText();
            // Assert
            Assert.Equal("поздравляю, ты получил исходный текст!!!", result);
        }

        [Fact]
        public void VigenereCipherHandlerIsNotNull()
        {
            // Arrange
            VigenereCipherHandler vigenereCipherHandler = new VigenereCipherHandler("", "");
            // Act
            string result = vigenereCipherHandler.DecodeText();
            // Assert
            Assert.NotNull(result);
        }
    }
}
