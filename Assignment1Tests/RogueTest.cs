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
    public class RogueTest
    {
        #region Rogue

        #region Instantiation

        [Fact]
        public void RogueConstructor_InitializeRogueWithName_ShouldCreateAnRogueWithTheName()
        {
            // Arrange
            string name = "Rogue";
            string expected = name;

            //Act 
            var rogue = new Rogue(name);
            string actual = rogue.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueConstructor_InitializeRogueWithName_ShouldCreateAnRogueAtLevel1()
        {
            // Arrange
            int expected = 1;

            //Act 
            var rogue = new Rogue("Rogue");
            var actual = rogue.Level;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueConstructor_InitializeRogueWithName_ShouldCreateARogueWithRogueLevel1Attributes()
        {
            // Arrange
            var expected = new HeroAttribute(2, 6, 1);

            //Act 
            var rogue = new Rogue("Rogue");
            var actual = rogue.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }

        #endregion Instantiation

        #region LevelUp

        [Fact]
        public void RogueLevelUp_CheckAttributesAfterLevelUp_ShouldHaveIncreasedWithTheExpectedValue()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = new HeroAttribute(3, 10, 2);

            //Act 
            rogue.LevelUp();
            var actual = rogue.LevelAttributes;

            // Assert
            Assert.True(expected.Equals(actual));
        }


        #endregion LevelUp

        #region Equip
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RogueEquip_TryToEquipHeadArmorPiecesOfDifferentLevels_ShouldEquipTheArmorPiece(int level)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            rogue.LevelUp();
            rogue.LevelUp();
            var armorPiece = new Armor("name", level, Slot.Head, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var expected = armorPiece;

            //Act 
            rogue.Equip(armorPiece);
            var equipment = rogue.GetEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueEquip_TryToEquipAHeadArmorPieceWhileWearingAHeadArmorPiece_ShouldEquipTheNewArmorPieceAndRemoveTheOldOne()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var armorPiece = new Armor("name", 1, Slot.Head, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("anotherName", 1, Slot.Head, ArmorType.Leather, new HeroAttribute(2, 2, 2));
            var expected = anotherArmorPiece;

            //Act 
            rogue.Equip(armorPiece);
            rogue.Equip(anotherArmorPiece);

            var equipment = rogue.GetEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RogueEquip_TryToEquipWeaponsOfDifferentLevels_ShouldEquipTheWeapon(int level)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            rogue.LevelUp();
            rogue.LevelUp();
            var weapon = new Weapon("name", level, WeaponType.Daggers, 1);
            var expected = weapon;

            //Act 
            rogue.Equip(weapon);
            var equipment = rogue.GetEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Swords)]
        [InlineData(WeaponType.Daggers)]
        public void RogueEquip_TryToEquipWeaponsOfDifferentTypes_ShouldEquipTheWeapon(WeaponType weaponType)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var weapon = new Weapon("name", 1, weaponType, 1);
            var expected = weapon;

            //Act 
            rogue.Equip(weapon);
            var equipment = rogue.GetEquipment();
            var actual = equipment[0];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueEquip_TryToEquipWeaponsWhenAlreadyHavingAWeapon_ShouldEquipTheNewWeaponAndRemoveTheOldWeapon()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var weapon = new Weapon("name", 1, WeaponType.Daggers, 1);
            var anotherWeapon = new Weapon("anotherName", 1, WeaponType.Daggers, 2);
            var expected = anotherWeapon;

            //Act 
            rogue.Equip(weapon);
            rogue.Equip(anotherWeapon);
            var equipment = rogue.GetEquipment();
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
        public void RogueEquip_TryToEquipArmorPiecesOfTooHighLevel_ShouldReturnAException(Slot armorPieceSlot)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = "You require a higher level to equip that armor! (required level 2)";
            var armorPiece = new Armor("name", 2, armorPieceSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => rogue.Equip(armorPiece));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(ArmorType.Plate, "Plate")]
        [InlineData(ArmorType.Cloth, "Cloth")]
        public void RogueEquip_TryToEquipHeadArmorPiecesOfWrongTypes_ShouldReturnAException(ArmorType armorType, string type)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = $"You can't wear armors of that type! ({type})";
            var headPiece = new Armor("name", 1, Slot.Head, armorType, new HeroAttribute(1, 1, 1));

            //Act 
            var actual = Assert.Throws<InvalidArmorException>(() => rogue.Equip(headPiece)).Message;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Daggers)]
        [InlineData(WeaponType.Swords)]
        public void RogueEquip_TryToEquipWeaponOfTooHighLevel_ShouldReturnAException(WeaponType type)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = "You require a higher level to equip that weapon! (required level 2)";
            var weapon = new Weapon("name", 2, type, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => rogue.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        [Theory]
        [InlineData(WeaponType.Staffs, "Staffs")]
        [InlineData(WeaponType.Wands, "Wands")]
        [InlineData(WeaponType.Bows, "Bows")]
        [InlineData(WeaponType.Hammers, "Hammers")]
        [InlineData(WeaponType.Axes, "Axes")]
        public void RogueEquip_TryToEquipWeaponOfWrongTypes_ShouldReturnAException(WeaponType weaponType, string type)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = $"You can't use weapons of that type! ({type})";
            var weapon = new Weapon("name", 1, weaponType, 1);

            //Act 
            var actual = Assert.Throws<InvalidWeaponException>(() => rogue.Equip(weapon));

            // Assert
            Assert.Equal(expected, actual.Message);
        }

        #endregion EquipException

        #region TotalAttribute
        [Fact]
        public void RogueTotalAttribute_CheckTotalAttributeOnALevel1RogueWithNoEquipment_ShouldReturnSpecificValuesForAllAttribute()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = new HeroAttribute(2, 6, 1);

            //Act 
            var actual = rogue.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Body)]
        [InlineData(Slot.Legs)]
        public void RogueTotalAttribute_CheckTotalAttributeOnALevel1RogueWithOnePieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var armor = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var expected = new HeroAttribute(3, 7, 2);

            //Act 
            rogue.Equip(armor);
            var actual = rogue.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head, Slot.Body)]
        [InlineData(Slot.Head, Slot.Legs)]
        [InlineData(Slot.Body, Slot.Legs)]
        public void RogueTotalAttribute_CheckTotalAttributeOnALevel1RogueWithTwoPieceOfEquipment_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot, Slot anotherArmorSlot)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, anotherArmorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var expected = new HeroAttribute(4, 8, 3);

            //Act 
            rogue.Equip(armorPiece);
            rogue.Equip(anotherArmorPiece);
            var actual = rogue.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Legs)]
        [InlineData(Slot.Body)]
        public void RogueTotalAttribute_CheckTotalAttributeOnALevel1RogueAfterChangingArmor_ShouldReturnSpecificValuesForAllAttribute(Slot armorSlot)
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var armorPiece = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(1, 1, 1));
            var anotherArmorPiece = new Armor("armor", 1, armorSlot, ArmorType.Leather, new HeroAttribute(2, 2, 2));
            var expected = new HeroAttribute(4, 8, 3);

            //Act 
            rogue.Equip(armorPiece);
            rogue.Equip(anotherArmorPiece);
            var actual = rogue.TotalAttributes();

            // Assert
            Assert.True(expected.Equals(actual));
        }
        #endregion TotalAttribute

        #region Damage
        [Fact]
        public void RogueDamage_WithoutWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = 1 * (1 + 6 / 100);

            //Act 
            var actual = rogue.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueDamage_WithoutWeaponAndAtLevel2_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var expected = 1 * (1 + 10 / 100);

            //Act 
            rogue.LevelUp();
            var actual = rogue.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueDamage_WithWeaponAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var weapon = new Weapon("weapon", 1, WeaponType.Daggers, 3);
            var expected = 3 * (1 + 6 / 100);

            //Act
            rogue.Equip(weapon);
            var actual = rogue.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueDamage_WithWeaponChangedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var weapon = new Weapon("weapon", 1, WeaponType.Daggers, 3);
            var anotherWeapon = new Weapon("weapon", 1, WeaponType.Daggers, 5);
            var expected = 5 * (1 + 6 / 100);

            //Act
            rogue.Equip(weapon);
            rogue.Equip(anotherWeapon);
            var actual = rogue.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RogueDamage_WithWeaponAndArmorEquippedAndAtLevel1_ShouldDealASpecificAmountOfDamageBasedOnparameters()
        {
            // Arrange
            var rogue = new Rogue("Rogue");
            var weapon = new Weapon("weapon", 1, WeaponType.Daggers, 3);
            var helmet = new Armor("helmet", 1, Slot.Head, ArmorType.Leather, new HeroAttribute(0, 1, 0));
            var body = new Armor("body", 1, Slot.Body, ArmorType.Leather, new HeroAttribute(0, 1, 0));
            var expected = 3 * (1 + 8 / 100);

            //Act
            rogue.Equip(weapon);
            rogue.Equip(helmet);
            rogue.Equip(body);
            var actual = rogue.Damage();

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion Damage

        #region display

        [Fact]
        public void Display_WhenCalledOnRogue_ShouldReturnAStringContainingBasicInfo()
        {
            // Arrange
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Name: Rogue");
            stringBuilder.AppendLine($"Class: Rogue");
            stringBuilder.AppendLine($"Level: 1");
            stringBuilder.AppendLine($"Total strength: 2");
            stringBuilder.AppendLine($"Total dexterity: 6");
            stringBuilder.AppendLine($"Total intelligence: 1");
            stringBuilder.AppendLine($"Damage: 1");
            string expected = stringBuilder.ToString();

            //Act 
            var rogue = new Rogue("Rogue");
            string actual = rogue.Display();

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion display

        #endregion Rogue
    }
}
