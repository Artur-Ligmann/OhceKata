using Moq;
using NUnit.Framework;
using System;
namespace Ohce.Tests
{
    [TestFixture]
    public class OhceTestFixture
    {
        [Test]
        public void ThrowsExceptionWithNullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new Ohce("artur", new DateTime(2016, 10, 21, 22, 0, 0), null));
        }

        [Test]
        public void ThrowsExceptionWhenNameNotProvided()
        {
            Assert.Throws<ArgumentNullException>(() => new Ohce(String.Empty, new DateTime(2016, 10, 21, 22, 0, 0), new Mock<IConsole>().Object));
        }

        [Test]
        public void EchoesReversedInputWhenNoCommandSpecified()
        {

            // Arrange
            var console = BuildConsoleWithInput("artur");
            var sut = new Ohce("Artur", new DateTime(2016, 10, 21, 22, 0, 0), console.Object);

            // Act
            sut.Run();

            // Assert
            console.Verify(c => c.Write("rutra"), Times.Once);
        }

        [Test]
        [TestCase("Artur", 22, "Buenas noches Artur")]
        [TestCase("Piotr", 7, "Buenos días Piotr")]
        [TestCase("Piotr", 14, "Buenas tardes Piotr")]
        public void GreetsUserWithCorrectGreet(string userName, int hour, string greetingText)
        {
            // Arrange
            var console = BuildConsoleWithInput("");
            var sut = new Ohce(userName, new DateTime(2016, 10, 21, hour, 0, 0), console.Object);

            // Act
            sut.Run();

            // Assert
            console.Verify(c => c.Write(greetingText), Times.Once);
        }

        [Test]
        public void ExitsApplicationWithMessageOnProperCommand()
        {
            // Arramge
            var console = BuildConsoleWithInput("Stop!");
            var sut = new Ohce("Artur", new DateTime(2016, 10, 21, 22, 0, 0), console.Object);
        
            // Act
            sut.Run();

            // Verify
            console.Verify(c => c.Write("Adios Artur"));
            Assert.That(sut.Finished == true);
        }

        
        [Test]
        public void ReactsToPalindromeWithProperReaction()
        {
            // Arrange
            var console = BuildConsoleWithInput("oto") ;
            var sut = new Ohce("Artur", new DateTime(2016, 10, 21, 22, 0, 0), console.Object);

            // Act
            sut.Run();

            // Assert
            console.Verify(c => c.Write("oto"));
            console.Verify(c => c.Write("¡Bonita palabra!"));
        }

        [Test]
        public void GreetsOnlyFirstTime()
        {
            // Arrange
            var console = BuildConsoleWithInput("oto");
            var sut = new Ohce("Artur", new DateTime(2016, 10, 21, 22, 0, 0), console.Object);

            /// Act
            sut.Run();
            sut.Run();

            // Assert
            console.Verify(c => c.Write("Buenas noches Artur"), Times.Once);
        }
        private static Mock<IConsole> BuildConsoleWithInput(string input)
        {
            var console = new Mock<IConsole>();
            console.Setup(c => c.ReadLine()).Returns(input);
            return console;
        }

    }

    
}
