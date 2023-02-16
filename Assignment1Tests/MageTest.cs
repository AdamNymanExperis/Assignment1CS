using Assignment1.Enums;
using Assignment1.Exceptions;
using Assignment1.Helper;
using Assignment1.Heroes;
using Assignment1.Items;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1Tests
{
    public class MageTest
    {
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

        #region LevelUp

        [Fact]
        public void MageLevelUp_CheckAttributesAfterLevelUp_ShouldHaveIncreasedWithTheExpectedValue()
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = new HeroAttribute(2, 2, 13);

            //Act 
            mage.LevelUp();
            var actual = mage.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }


        #endregion LevelUp

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
            var equipment = mage.GetEquipment();
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

            var equipment = mage.GetEquipment();
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
            var equipment = mage.GetEquipment();
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
            var equipment = mage.GetEquipment();
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
            var equipment = mage.GetEquipment();
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
            var armorPiece = new Armor("name", 2, armorPieceSlot, ArmorType.Cloth, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => mage.Equip(armorPiece)).Message;

            // Assert
            Assert.Equal(expected, actual);
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
            var actual = Assert.Throws<InvalidWeaponException>(() => mage.Equip(weapon)).Message;

            // Assert
            Assert.Equal(expected, actual);
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
            var actual = Assert.Throws<InvalidWeaponException>(() => mage.Equip(weapon)).Message;

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion EquipException

        #region TotalAttribute
        [Fact]
        public void MageTotalAttribute_CheckTotalAttributeOnALevel1MageWithNoEquipment_ShouldReturnSpecificValuesForAllAttribute()
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = new HeroAttribute(1, 1, 8);

            //Act 
            var actual = mage.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Body)]
        [InlineData(Slot.Legs)]
        public void MageTotalAttribute_CheckTotalAttributeOnALevel1MageWithOnePieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var mage = new Mage("Mage");
            var armor = new Armor("armor", 1, armorSlot, ArmorType.Cloth, new HeroAttribute(1,1,1));
            var expected = new HeroAttribute(2, 2, 9);

            //Act 
            mage.Equip(armor);
            var actual = mage.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head, Slot.Body)]
        [InlineData(Slot.Head, Slot.Legs)]
        [InlineData(Slot.Body, Slot.Legs)]
        public void MageTotalAttribute_CheckTotalAttributeOnALevel1MageWithTwoPieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot, Slot anotherArmorSlot)
        {
            // Arrange
            var mage = new Mage("Mage");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Cloth, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, anotherArmorSlot, ArmorType.Cloth, new HeroAttribute(1, 1, 1));
            var expected = new HeroAttribute(3, 3, 10);

            //Act 
            mage.Equip(armorPiece);
            mage.Equip(anotherArmorPiece);
            var actual = mage.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Legs)]
        [InlineData(Slot.Body)]
        public void MageTotalAttribute_CheckTotalAttributeOnALevel1MageAfterChangingArmor_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var mage = new Mage("Mage");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Cloth, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, armorSlot, ArmorType.Cloth, new HeroAttribute(2, 2, 2));
            var expected = new HeroAttribute(3, 3, 10);

            //Act 
            mage.Equip(armorPiece);
            mage.Equip(anotherArmorPiece);
            var actual = mage.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }
        #endregion TotalAttribute

        #region Damage
        [Fact]
        public void MageDamage_WithoutWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = 1 * (1 +  8/100);

            //Act 
            var actual = mage.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MageDamage_WithoutWeaponAndAtLevel2_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var mage = new Mage("Mage");
            var expected = 1 * (1 + 13 / 100);

            //Act 
            mage.LevelUp();
            var actual = mage.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MageDamage_WithWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var mage = new Mage("Mage");
            var weapon = new Weapon("weapon",1,WeaponType.Wands,3);
            var expected = 3 * (1 + 8 / 100);

            //Act
            mage.Equip(weapon);
            var actual = mage.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MageDamage_WithWeaponChangedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var mage = new Mage("Mage");
            var weapon = new Weapon("weapon", 1, WeaponType.Wands, 3);
            var anotherWeapon = new Weapon("weapon", 1, WeaponType.Wands, 5);
            var expected = 5 * (1 + 8 / 100);

            //Act
            mage.Equip(weapon);
            mage.Equip(anotherWeapon);
            var actual = mage.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MageDamage_WithWeaponAndArmorEquippedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var mage = new Mage("Mage");
            var weapon = new Weapon("weapon", 1, WeaponType.Wands, 3);
            var helmet = new Armor("helmet", 1, Slot.Head, ArmorType.Cloth, new HeroAttribute(0,0,1));
            var body = new Armor("body", 1, Slot.Body, ArmorType.Cloth, new HeroAttribute(0,0,1));
            var expected = 3 * (1 + 10 / 100);

            //Act
            mage.Equip(weapon);
            mage.Equip(helmet);
            mage.Equip(body);
            var actual = mage.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion Damage

        #region display

        [Fact]
        public void Display_WhenCalledOnMage_ShouldReturnAStringContainingBasicInfo()
        {
            // Arrange
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Name: Mage");
            stringBuilder.AppendLine($"Class: Mage");
            stringBuilder.AppendLine($"Level: 1");
            stringBuilder.AppendLine($"Total strength: 1");
            stringBuilder.AppendLine($"Total dexterity: 1");
            stringBuilder.AppendLine($"Total intelligence: 8");
            stringBuilder.AppendLine($"Damage: 1");
            string expected = stringBuilder.ToString();

            //Act 
            var mage = new Mage("Mage");
            string actual = mage.Display();

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion display

        #endregion Mage
    }
}
