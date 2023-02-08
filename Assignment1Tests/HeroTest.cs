using Assignment1.Heroes;
using Moq;
using Xunit.Sdk;

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
        public void Constructor_InitializeWithName_ShouldCreateAHeroWithoutAttributes()
        {
            // Arrange
            var expected = new int[] {0,0,0};

            //Act 
            var mock = new Mock<Hero>("Hero");
            var actual = mock.Object.LevelAttributes.GetAttributes();

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

        [Fact]
        public void LevelUp_WhenCalled_ShouldIncreaseLevelBy1()
        {
            // Arrange
            string name = "Hero";
            var mock = new Mock<Hero>("Hero");
            mock.Object.LevelUp();
            int expected = 2;

            //Act 
            var actual = mock.Object.Level;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TotalAttributes_WhenCalled_ShouldReturnZeroWhenZeroAttributesAndNoEquipments()
        {
            // Arrange
            string name = "Hero";
            var mock = new Mock<Hero>("Hero");
            var expected = new int[] { 0, 0, 0 };

            //Act 
            var total = mock.Object.TotalAttributes();
            var actual = total.GetAttributes();

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}