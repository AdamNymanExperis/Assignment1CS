using Assignment1.Enums;
using Assignment1.Helper;
using Assignment1.Heroes;
using Assignment1.Items;
using Moq;
using Moq.Protected;
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

        #endregion Hero

        #region Mage



        #endregion Mage
        [Fact]
        public void TotalAttributes_WhenCalledAfterEquipingHeadArmor_ShouldNotReturnZeroWhenAnHeadArmorPieceIsEquipped()
        {
            // Arrange
            string name = "Hero";
            var mock = new Mock<Hero>("Hero");
            var armor = new Armor("name", 1, Slot.Head, ArmorType.Cloth, new HeroAttribute(1, 2, 3));
            var expected = new HeroAttribute(1, 2, 3);

            //Act 
            var actual = mock.Object.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }
    }
}