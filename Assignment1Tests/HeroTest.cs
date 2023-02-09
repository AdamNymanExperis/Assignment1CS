using Assignment1.Enums;
using Assignment1.Exceptions;
using Assignment1.Helper;
using Assignment1.Heroes;
using Assignment1.Items;
using Moq;
using Moq.Protected;
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

        #endregion Hero

        #region Mage

        #region Instantiation

        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAnMageWithTheName()
        {
            // Arrange
            string name = "Mage";
            string expected = name;

            //Act 
            var mage = new Mage(name);
            string actual = mage.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAnMageAtLevel1()
        {
            // Arrange
            int expected = 1;

            //Act 
            var mage = new Mage("Mage");
            var actual = mage.Level;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_InitializeWithName_ShouldCreateAMageWithMageLevel1Attributes()
        {
            // Arrange
            var expected = new HeroAttribute(1, 1, 8);

            //Act 
            var mage = new Mage("Mage");
            var actual = mage.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }

        #endregion Instantiation

        #region EquipException
        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Body)]
        [InlineData(Slot.Legs)]
        public void MageEquip_TryToEquipArmorPiecesOfTooHighLevel_ShouldReturnAException(Slot armorPieceSlot)
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = "You require a higher level to equip that armor! (required level 2)";
            var armorPiece = new Armor("name", 2, armorPieceSlot, ArmorType.Cloth, new HeroAttribute(1,1,1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>( () => mage.Equip(armorPiece));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(ArmorType.Mail, "Mail")]
        [InlineData(ArmorType.Plate, "Plate")]
        [InlineData(ArmorType.Leather, "Leather")]
        public void MageEquip_TryToEquipHeadArmorPiecesOfWrongTypes_ShouldReturnAException(ArmorType armorType, string type)
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = $"You can't wear armors of that type! ({type})";
            var headPiece = new Armor("name", 1, Slot.Head, armorType, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => mage.Equip(headPiece)).Message;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Staffs)]
        [InlineData(WeaponType.Wands)]
        public void MageEquip_TryToEquipWeaponOfTooHighLevel_ShouldReturnAException(WeaponType type)
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = "You require a higher level to equip that weapon! (required level 2)";
            var weapon = new Weapon("name", 2, Slot.Weapon, type, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => mage.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(WeaponType.Axes, "Axes")]
        [InlineData(WeaponType.Bows, "Bows")]
        [InlineData(WeaponType.Daggers, "Daggers")]
        [InlineData(WeaponType.Swords, "Swords")]
        public void MageEquip_TryToEquipWeaponOfWrongTypes_ShouldReturnAException(WeaponType weaponType, string type)
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = $"You can't use weapons of that type! ({type})";
            var weapon = new Weapon("name", 1, Slot.Weapon, weaponType, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => mage.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }



        #endregion EquipException

        #region TotalAttributes
        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Body)]
        [InlineData(Slot.Legs)]
        public void TotalAttributes_GetTotalAttributeWithArmorEquiped_ShouldReturnAExeption(Slot armorPieceSlot)
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = "You require a higher level to equip that armor! (required level 2)";
            var headPiece = new Armor("name", 2, armorPieceSlot, ArmorType.Cloth, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => mage.Equip(headPiece));

            // Assert
            Assert.Equal(expected, actual.Message);
        }
        #endregion TotalAttributes

        #endregion Mage
    }
}