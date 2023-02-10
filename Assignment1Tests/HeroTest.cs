using Assignment1.Enums;
using Assignment1.Exceptions;
using Assignment1.Helper;
using Assignment1.Heroes;
using Assignment1.Items;
using Moq;
using Moq.Protected;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using Xunit.Sdk;

namespace Assignment1Tests
{
    public class HeroTest
    {
        #region Hero

        #region Instantiation

        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAnHeroWithTheName()
        {
            // Arrange
            string name = "Hero";
            string expected = name;

            //Act 
            var mock = new Mock<Hero>("Hero");
            string actual = mock.Object.Name;

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAnHeroAtLevel1()
        {
            // Arrange
            int expected = 1;

            //Act 
            var mock = new Mock<Hero>("Hero");
            var actual = mock.Object.Level;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAHeroWithoutAttributes()
        {
            // Arrange
            var expected = new HeroAttribute(0,0,0);

            //Act 
            var mock = new Mock<Hero>("Hero");
            var actual = mock.Object.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }

        #endregion Instantiation

        #region TotalAttributes
        [Fact]
        public void TotalAttributes_WhenCalled_ShouldReturnZeroForHeroWithoutClassAndArmor()
        {
            // Arrange
            string name = "Hero";
            var mock = new Mock<Hero>("Hero");
            var expected = new HeroAttribute(0,0,0);

            //Act 
            var actual = mock.Object.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        #endregion TotalAttributes

        #region display

        [Fact]
        public void Display_WhenCalledOnHero_ShouldReturnAStringContainingBasicInfo()
        {
            // Arrange
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Name: Hero");
            stringBuilder.AppendLine($"Class: none");
            stringBuilder.AppendLine($"Level: 1");
            stringBuilder.AppendLine($"Total strength: 0");
            stringBuilder.AppendLine($"Total dexterity: 0");
            stringBuilder.AppendLine($"Total intelligence: 0");
            stringBuilder.AppendLine($"Damage: 1");
            string expected = stringBuilder.ToString();

            //Act 
            var mock = new Mock<Hero>("Hero");
            mock.Setup(m => m.Damage()).Returns(1); // since Moq overrides methods I have to set this method to return 1 so that it doesnt return 0 
            string actual = mock.Object.Display();

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion display

        #endregion Hero
    }
}
