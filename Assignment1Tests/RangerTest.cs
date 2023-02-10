using Assignment1.Enums;
using Assignment1.Exceptions;
using Assignment1.Helper;
using Assignment1.Heroes;
using Assignment1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1Tests
{
    public class RangerTest
    {
        #region Ranger

        #region Instantiation

        [Fact]
        public void RangerConstructor_InitializeRangerWithName_ShouldCreateAnRangerWithTheName()
        {
            // Arrange
            string name = "Ranger";
            string expected = name;

            //Act 
            var ranger = new Ranger(name);
            string actual = ranger.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerConstructor_InitializeRangerWithName_ShouldCreateAnRangerAtLevel1()
        {
            // Arrange
            int expected = 1;

            //Act 
            var ranger = new Ranger("Ranger");
            var actual = ranger.Level;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerConstructor_InitializeRangerWithName_ShouldCreateARangerWithRangerLevel1Attributes()
        {
            // Arrange
            var expected = new HeroAttribute(1, 7, 1);

            //Act 
            var ranger = new Ranger("Ranger");
            var actual = ranger.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }

        #endregion Instantiation

        #region LevelUp

        [Fact]
        public void RangerLevelUp_CheckAttributesAfterLevelUp_ShouldHaveIncreasedWithTheExpectedValue()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = new HeroAttribute(2, 12, 2);

            //Act 
            ranger.LevelUp();
            var actual = ranger.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }


        #endregion LevelUp

        #region Equip
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RangerEquip_TryToEquipHeadArmorPiecesOfDifferentLevels_ShouldEquipTheArmorPiece(int level)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            ranger.LevelUp();
            ranger.LevelUp();
            var armorPiece = new Armor("name", level, Slot.Head, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var expected = armorPiece;

            //Act 
            ranger.Equip(armorPiece);
            var equipment = ranger.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerEquip_TryToEquipAHeadArmorPieceWhileWearingAHeadArmorPiece_ShouldEquipTheNewArmorPieceAndRemoveTheOldOne()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var armorPiece = new Armor("name", 1, Slot.Head, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("anotherName", 1, Slot.Head, ArmorType.Leather, new HeroAttribute(2, 2, 2));
            var expected = anotherArmorPiece;

            //Act 
            ranger.Equip(armorPiece);
            ranger.Equip(anotherArmorPiece);

            var equipment = ranger.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RangerEquip_TryToEquipWeaponsOfDifferentLevels_ShouldEquipTheWeapon(int level)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            ranger.LevelUp();
            ranger.LevelUp();
            var weapon = new Weapon("name", level, WeaponType.Bows, 1);
            var expected = weapon;

            //Act 
            ranger.Equip(weapon);
            var equipment = ranger.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Bows)]
        public void RangerEquip_TryToEquipWeaponsOfDifferentTypes_ShouldEquipTheWeapon(WeaponType weaponType)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var weapon = new Weapon("name", 1, weaponType, 1);
            var expected = weapon;

            //Act 
            ranger.Equip(weapon);
            var equipment = ranger.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerEquip_TryToEquipWeaponsWhenAlreadyHavingAWeapon_ShouldEquipTheNewWeaponAndRemoveTheOldWeapon()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var weapon = new Weapon("name", 1, WeaponType.Bows, 1);
            var anotherWeapon = new Weapon("anotherName", 1, WeaponType.Bows, 2);
            var expected = anotherWeapon;

            //Act 
            ranger.Equip(weapon);
            ranger.Equip(anotherWeapon);
            var equipment = ranger.getEquipment();
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
        public void RangerEquip_TryToEquipArmorPiecesOfTooHighLevel_ShouldReturnAException(Slot armorPieceSlot)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = "You require a higher level to equip that armor! (required level 2)";
            var armorPiece = new Armor("name", 2, armorPieceSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => ranger.Equip(armorPiece));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(ArmorType.Plate, "Plate")]
        [InlineData(ArmorType.Cloth, "Cloth")]
        public void RangerEquip_TryToEquipHeadArmorPiecesOfWrongTypes_ShouldReturnAException(ArmorType armorType, string type)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = $"You can't wear armors of that type! ({type})";
            var headPiece = new Armor("name", 1, Slot.Head, armorType, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => ranger.Equip(headPiece)).Message;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Bows)]
        public void RangerEquip_TryToEquipWeaponOfTooHighLevel_ShouldReturnAException(WeaponType type)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = "You require a higher level to equip that weapon! (required level 2)";
            var weapon = new Weapon("name", 2, type, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => ranger.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(WeaponType.Daggers, "Daggers")]
        [InlineData(WeaponType.Staffs, "Staffs")]
        [InlineData(WeaponType.Wands, "Wands")]
        [InlineData(WeaponType.Swords, "Swords")]
        [InlineData(WeaponType.Hammers, "Hammers")]
        [InlineData(WeaponType.Axes, "Axes")]
        public void RangerEquip_TryToEquipWeaponOfWrongTypes_ShouldReturnAException(WeaponType weaponType, string type)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = $"You can't use weapons of that type! ({type})";
            var weapon = new Weapon("name", 1, weaponType, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => ranger.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        #endregion EquipException

        #region TotalAttribute
        [Fact]
        public void RangerTotalAttribute_CheckTotalAttributeOnALevel1RangerWithNoEquipment_ShouldReturnSpecificValuesForAllAttribute()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = new HeroAttribute(1, 7, 1);

            //Act 
            var actual = ranger.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Body)]
        [InlineData(Slot.Legs)]
        public void RangerTotalAttribute_CheckTotalAttributeOnALevel1RangerWithOnePieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var armor = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var expected = new HeroAttribute(2, 8, 2);

            //Act 
            ranger.Equip(armor);
            var actual = ranger.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head, Slot.Body)]
        [InlineData(Slot.Head, Slot.Legs)]
        [InlineData(Slot.Body, Slot.Legs)]
        public void RangerTotalAttribute_CheckTotalAttributeOnALevel1RangerWithTwoPieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot, Slot anotherArmorSlot)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, anotherArmorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var expected = new HeroAttribute(3, 9, 3);

            //Act 
            ranger.Equip(armorPiece);
            ranger.Equip(anotherArmorPiece);
            var actual = ranger.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Legs)]
        [InlineData(Slot.Body)]
        public void RangerTotalAttribute_CheckTotalAttributeOnALevel1RangerAfterChangingArmor_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(2, 2, 2));
            var expected = new HeroAttribute(3, 9, 3);

            //Act 
            ranger.Equip(armorPiece);
            ranger.Equip(anotherArmorPiece);
            var actual = ranger.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }
        #endregion TotalAttribute

        #region Damage
        [Fact]
        public void RangerDamage_WithoutWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = 1 * (1 + 7 / 100);

            //Act 
            var actual = ranger.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerDamage_WithoutWeaponAndAtLevel2_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var expected = 1 * (1 + 12 / 100);

            //Act 
            ranger.LevelUp();
            var actual = ranger.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerDamage_WithWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var weapon = new Weapon("weapon", 1, WeaponType.Bows, 3);
            var expected = 3 * (1 + 7 / 100);

            //Act
            ranger.Equip(weapon);
            var actual = ranger.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerDamage_WithWeaponChangedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var weapon = new Weapon("weapon", 1, WeaponType.Bows, 3);
            var anotherWeapon = new Weapon("weapon", 1, WeaponType.Bows, 5);
            var expected = 5 * (1 + 7 / 100);

            //Act
            ranger.Equip(weapon);
            ranger.Equip(anotherWeapon);
            var actual = ranger.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RangerDamage_WithWeaponAndArmorEquippedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var ranger = new Ranger("Ranger");
            var weapon = new Weapon("weapon", 1, WeaponType.Bows, 3);
            var helmet = new Armor("helmet", 1, Slot.Head, ArmorType.Leather, new HeroAttribute(0, 1, 0));
            var body = new Armor("body", 1, Slot.Body, ArmorType.Leather, new HeroAttribute(0, 1, 0));
            var expected = 3 * (1 + 9 / 100);

            //Act
            ranger.Equip(weapon);
            ranger.Equip(helmet);
            ranger.Equip(body);
            var actual = ranger.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion Damage

        #endregion Ranger
    }
}
