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
        public void MageConstructor_InitializeMageWithName_ShouldCreateAnMageWithTheName()
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
        public void MageConstructor_InitializeMageWithName_ShouldCreateAnMageAtLevel1()
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
        public void MageConstructor_InitializeMageWithName_ShouldCreateAMageWithMageLevel1Attributes()
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

        #region Equip
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void MageEquip_TryToEquipHeadArmorPiecesOfDifferentLevels_ShouldEquipTheArmorPiece(int level)
        {
            // Arrange
            var mage = new Mage("Mage");
            mage.LevelUp();
            mage.LevelUp();
            var armorPiece = new Armor("name", level, Slot.Head, ArmorType.Cloth, new HeroAttribute(1, 1, 1));
            var expected = armorPiece; 

            //Act 
            mage.Equip(armorPiece);
            var equipment = mage.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MageEquip_TryToEquipAHeadArmorPieceWhileWearingAHeadArmorPiece_ShouldEquipTheNewArmorPieceAndRemoveTheOldOne()
        {
            // Arrange
            var mage = new Mage("Mage");
            var armorPiece = new Armor("name", 1, Slot.Head, ArmorType.Cloth, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("anotherName", 1, Slot.Head, ArmorType.Cloth, new HeroAttribute(2, 2, 2));
            var expected = anotherArmorPiece;

            //Act 
            mage.Equip(armorPiece);
            mage.Equip(anotherArmorPiece);

            var equipment = mage.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void MageEquip_TryToEquipWeaponsOfDifferentLevels_ShouldEquipTheWeapon(int level)
        {
            // Arrange
            var mage = new Mage("Mage");
            mage.LevelUp();
            mage.LevelUp();
            var weapon = new Weapon("name", level, WeaponType.Staffs, 1);
            var expected = weapon;

            //Act 
            mage.Equip(weapon);
            var equipment = mage.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Staffs)]
        [InlineData(WeaponType.Wands)]
        public void MageEquip_TryToEquipWeaponsOfDifferentTypes_ShouldEquipTheWeapon(WeaponType weaponType)
        {
            // Arrange
            var mage = new Mage("Mage");
            var weapon = new Weapon("name", 1, weaponType, 1);
            var expected = weapon;

            //Act 
            mage.Equip(weapon);
            var equipment = mage.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MageEquip_TryToEquipWeaponsWhenAlreadyHavingAWeapon_ShouldEquipTheNewWeaponAndRemoveTheOldWeapon()
        {
            // Arrange
            var mage = new Mage("Mage");
            var weapon = new Weapon("name", 1, WeaponType.Staffs, 1);
            var anotherWeapon = new Weapon("anotherName", 1, WeaponType.Staffs, 2);
            var expected = anotherWeapon;

            //Act 
            mage.Equip(weapon);
            mage.Equip(anotherWeapon);
            var equipment = mage.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion Equip

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
            var weapon = new Weapon("name", 2, type, 1);

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
            var weapon = new Weapon("name", 1, weaponType, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => mage.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        #endregion EquipException

        

        #endregion Mage
    }
}