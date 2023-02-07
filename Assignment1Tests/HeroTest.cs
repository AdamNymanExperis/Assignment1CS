using Assignment1;
using Moq;

namespace Assignment1Tests
{
    public class HeroTest
    {
        #region Instantiation
        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAnHeroWithTheName()
        {
            // Arrange
            string name = "Hero";
            string expected = name;

            //Act 
            var mock = new Mock<Hero>("Hero");
            var actual = mock.Object.Name;

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
        public void Constructor_InitializeWithName_ShouldCreateAHeroWithoutItems()
        {
            // Arrange
            var expected = new Item[0];

            //Act 
            var mock = new Mock<Hero>("Hero");
            var actual = mock.Object.Equipment.ToArray();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAHeroNoValidWeaponType()
        {
            // Arrange
            var expected = new String[0];

            //Act 
            var mock = new Mock<Hero>("Hero");
            var actual = mock.Object.ValidWeaponTypes;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAHeroNoValidArmorType()
        {
            // Arrange
            var expected = new String[0];

            //Act 
            var mock = new Mock<Hero>("Hero");
            var actual = mock.Object.ValidArmorTypes;

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion Instantiation
    }
}