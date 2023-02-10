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
    public class WarriorTest
    {
        #region Warrior

        #region Instantiation

        [Fact]
        public void WarriorConstructor_InitializeWarriorWithName_ShouldCreateAnWarriorWithTheName()
        {
            // Arrange
            string name = "Warrior";
            string expected = name;

            //Act 
            var warrior = new Warrior(name);
            string actual = warrior.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorConstructor_InitializeWarriorWithName_ShouldCreateAnWarriorAtLevel1()
        {
            // Arrange
            int expected = 1;

            //Act 
            var warrior = new Warrior("Warrior");
            var actual = warrior.Level;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorConstructor_InitializeWarriorWithName_ShouldCreateAWarriorWithWarriorLevel1Attributes()
        {
            // Arrange
            var expected = new HeroAttribute(5, 2, 1);

            //Act 
            var warrior = new Warrior("Warrior");
            var actual = warrior.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }

        #endregion Instantiation

        #region LevelUp

        [Fact]
        public void WarriorLevelUp_CheckAttributesAfterLevelUp_ShouldHaveIncreasedWithTheExpectedValue()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = new HeroAttribute(8, 4, 2);

            //Act 
            warrior.LevelUp();
            var actual = warrior.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }


        #endregion LevelUp

        #region Equip
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WarriorEquip_TryToEquipHeadArmorPiecesOfDifferentLevels_ShouldEquipTheArmorPiece(int level)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            warrior.LevelUp();
            warrior.LevelUp();
            var armorPiece = new Armor("name", level, Slot.Head, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var expected = armorPiece;

            //Act 
            warrior.Equip(armorPiece);
            var equipment = warrior.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorEquip_TryToEquipAHeadArmorPieceWhileWearingAHeadArmorPiece_ShouldEquipTheNewArmorPieceAndRemoveTheOldOne()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var armorPiece = new Armor("name", 1, Slot.Head, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("anotherName", 1, Slot.Head, ArmorType.Plate, new HeroAttribute(2, 2, 2));
            var expected = anotherArmorPiece;

            //Act 
            warrior.Equip(armorPiece);
            warrior.Equip(anotherArmorPiece);

            var equipment = warrior.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WarriorEquip_TryToEquipWeaponsOfDifferentLevels_ShouldEquipTheWeapon(int level)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            warrior.LevelUp();
            warrior.LevelUp();
            var weapon = new Weapon("name", level, WeaponType.Hammers, 1);
            var expected = weapon;

            //Act 
            warrior.Equip(weapon);
            var equipment = warrior.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Axes)]
        [InlineData(WeaponType.Hammers)]
        [InlineData(WeaponType.Swords)]
        public void WarriorEquip_TryToEquipWeaponsOfDifferentTypes_ShouldEquipTheWeapon(WeaponType weaponType)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var weapon = new Weapon("name", 1, weaponType, 1);
            var expected = weapon;

            //Act 
            warrior.Equip(weapon);
            var equipment = warrior.getEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorEquip_TryToEquipWeaponsWhenAlreadyHavingAWeapon_ShouldEquipTheNewWeaponAndRemoveTheOldWeapon()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var weapon = new Weapon("name", 1, WeaponType.Axes, 1);
            var anotherWeapon = new Weapon("anotherName", 1, WeaponType.Axes, 2);
            var expected = anotherWeapon;

            //Act 
            warrior.Equip(weapon);
            warrior.Equip(anotherWeapon);
            var equipment = warrior.getEquipment();
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
        public void WarriorEquip_TryToEquipArmorPiecesOfTooHighLevel_ShouldReturnAException(Slot armorPieceSlot)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = "You require a higher level to equip that armor! (required level 2)";
            var armorPiece = new Armor("name", 2, armorPieceSlot, ArmorType.Plate, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => warrior.Equip(armorPiece));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(ArmorType.Leather, "Leather")]
        [InlineData(ArmorType.Cloth, "Cloth")]
        public void WarriorEquip_TryToEquipHeadArmorPiecesOfWrongTypes_ShouldReturnAException(ArmorType armorType, string type)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = $"You can't wear armors of that type! ({type})";
            var headPiece = new Armor("name", 1, Slot.Head, armorType, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => warrior.Equip(headPiece)).Message;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Axes)]
        [InlineData(WeaponType.Hammers)]
        [InlineData(WeaponType.Swords)]
        public void WarriorEquip_TryToEquipWeaponOfTooHighLevel_ShouldReturnAException(WeaponType type)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = "You require a higher level to equip that weapon! (required level 2)";
            var weapon = new Weapon("name", 2, type, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => warrior.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(WeaponType.Daggers, "Daggers")]
        [InlineData(WeaponType.Staffs, "Staffs")]
        [InlineData(WeaponType.Wands, "Wands")]
        [InlineData(WeaponType.Bows, "Bows")]
        public void WarriorEquip_TryToEquipWeaponOfWrongTypes_ShouldReturnAException(WeaponType weaponType, string type)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = $"You can't use weapons of that type! ({type})";
            var weapon = new Weapon("name", 1, weaponType, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => warrior.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        #endregion EquipException

        #region TotalAttribute
        [Fact]
        public void WarriorTotalAttribute_CheckTotalAttributeOnALevel1WarriorWithNoEquipment_ShouldReturnSpecificValuesForAllAttribute()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = new HeroAttribute(5, 2, 1);

            //Act 
            var actual = warrior.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Body)]
        [InlineData(Slot.Legs)]
        public void WarriorTotalAttribute_CheckTotalAttributeOnALevel1WarriorWithOnePieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var armor = new Armor("armor", 1, armorSlot, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var expected = new HeroAttribute(6, 3, 2);

            //Act 
            warrior.Equip(armor);
            var actual = warrior.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head, Slot.Body)]
        [InlineData(Slot.Head, Slot.Legs)]
        [InlineData(Slot.Body, Slot.Legs)]
        public void WarriorTotalAttribute_CheckTotalAttributeOnALevel1WarriorWithTwoPieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot, Slot anotherArmorSlot)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, anotherArmorSlot, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var expected = new HeroAttribute(7, 4, 3);

            //Act 
            warrior.Equip(armorPiece);
            warrior.Equip(anotherArmorPiece);
            var actual = warrior.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Legs)]
        [InlineData(Slot.Body)]
        public void WarriorTotalAttribute_CheckTotalAttributeOnALevel1WarriorAfterChangingArmor_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, armorSlot, ArmorType.Plate, new HeroAttribute(2, 2, 2));
            var expected = new HeroAttribute(7, 4, 3);

            //Act 
            warrior.Equip(armorPiece);
            warrior.Equip(anotherArmorPiece);
            var actual = warrior.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }
        #endregion TotalAttribute

        #region Damage
        [Fact]
        public void WarriorDamage_WithoutWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = 1 * (1 + 5 / 100);

            //Act 
            var actual = warrior.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorDamage_WithoutWeaponAndAtLevel2_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var expected = 1 * (1 + 8 / 100);

            //Act 
            warrior.LevelUp();
            var actual = warrior.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorDamage_WithWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var weapon = new Weapon("weapon", 1, WeaponType.Axes, 3);
            var expected = 3 * (1 + 5 / 100);

            //Act
            warrior.Equip(weapon);
            var actual = warrior.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorDamage_WithWeaponChangedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var weapon = new Weapon("weapon", 1, WeaponType.Axes, 3);
            var anotherWeapon = new Weapon("weapon", 1, WeaponType.Axes, 5);
            var expected = 5 * (1 + 5 / 100);

            //Act
            warrior.Equip(weapon);
            warrior.Equip(anotherWeapon);
            var actual = warrior.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WarriorDamage_WithWeaponAndArmorEquippedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var warrior = new Warrior("Warrior");
            var weapon = new Weapon("weapon", 1, WeaponType.Axes, 3);
            var helmet = new Armor("helmet", 1, Slot.Head, ArmorType.Plate, new HeroAttribute(1, 0, 0));
            var body = new Armor("body", 1, Slot.Body, ArmorType.Plate, new HeroAttribute(1, 0, 0));
            var expected = 3 * (1 + 7 / 100);

            //Act
            warrior.Equip(weapon);
            warrior.Equip(helmet);
            warrior.Equip(body);
            var actual = warrior.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion Damage

        #endregion Warrior
    }
}
