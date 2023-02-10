using Assignment1.Enums;
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
    public class ItemTest
    {
        #region Item
        [Fact]
        public void ItemConstructor_InitializeItem_ShouldCreateAnItemWithName()
        {
            // Arrange
            var expected = "Item";

            //Act 
            var mock = new Mock<Item>();
            var actual = mock.Object.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ItemConstructor_InitializeItem_ShouldCreateAnItemWithLevel1()
        {
            // Arrange
            var expected = 1;

            //Act 
            var mock = new Mock<Item>();
            var actual = mock.Object.RequiredLevel;

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion Item

        #region Armor
        [Fact]
        public void ArmorConstructor_InitializeArmorWithoutAnyParameters_ShouldCreateAnArmorWithStandardStats()
        {
            // Arrange
            var expected = new Armor("Gandalf's Wizard Hat", 95, Slot.Head, ArmorType.Cloth, new HeroAttribute(0, 0, 35));

            //Act 
            var actual = new Armor();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArmorConstructor_InitializeArmorWithParameters_ShouldCreateAnArmorWithSpecifiedName()
        {
            // Arrange
            var expected = "Helmet";

            //Act 
            var helmet = new Armor("Helmet", 1, Slot.Head, ArmorType.Plate, new HeroAttribute(1,1,1));
            var actual = helmet.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArmorConstructor_InitializeArmorWithParameters_ShouldCreateAnArmorWithSpecifiedLevel()
        {
            // Arrange
            var expected = 1;

            //Act 
            var helmet = new Armor("Helmet", 1, Slot.Head, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var actual = helmet.RequiredLevel;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(Slot.Head)]
        [InlineData(Slot.Body)]
        [InlineData(Slot.Legs)]
        public void ArmorConstructor_InitializeArmorWithParameters_ShouldCreateAnArmorWithSpecifiedSlot(Slot armorSlot)
        {
            // Arrange
            var expected = armorSlot;

            //Act 
            var armorPiece = new Armor("ArmorPiece", 1, armorSlot, ArmorType.Plate, new HeroAttribute(1, 1, 1));
            var actual = armorPiece.Slot;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(ArmorType.Cloth)]
        [InlineData(ArmorType.Leather)]
        [InlineData(ArmorType.Plate)]
        [InlineData(ArmorType.Mail)]
        public void ArmorConstructor_InitializeArmorWithParameters_ShouldCreateAnArmorWithSpecifiedType(ArmorType armorType)
        {
            // Arrange
            var expected = armorType;

            //Act 
            var armorPiece = new Armor("ArmorPiece", 1, Slot.Head, armorType, new HeroAttribute(1, 1, 1));
            var actual = armorPiece.ArmorType;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        
        public void ArmorConstructor_InitializeArmorWithParameters_ShouldCreateAnArmorWithSpecifiedAttributes()
        {
            // Arrange
            var expected = new HeroAttribute(1,1,1);

            //Act 
            var armorPiece = new Armor("ArmorPiece", 1, Slot.Head, ArmorType.Cloth, new HeroAttribute(1, 1, 1));
            var actual = armorPiece.ArmorAttribute;

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion Armor

        #region Weapon
        [Fact]
        public void WeaponConstructor_InitializeWeaponWithoutAnyParameters_ShouldCreateAnWeaponWithStandardStats()
        {
            // Arrange
            var expected = new Weapon("Sting, the Sword of Bilbo Baggins", 3, WeaponType.Swords, 4);
            
            //Act 
            var actual = new Weapon();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeaponConstructor_InitializeWeaponWithParameters_ShouldCreateAnWeaponWithSpecifiedName()
        {
            // Arrange
            var expected = "Sword";

            //Act 
            var sword = new Weapon("Sword", 1, WeaponType.Swords,1) ;
            var actual = sword.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeaponConstructor_InitializeWeaponWithParameters_ShouldCreateAnWeaponWithSpecifiedLevel()
        {
            // Arrange
            var expected = 1;

            //Act 
            var sword = new Weapon("Sword", 1, WeaponType.Swords, 1);
            var actual = sword.RequiredLevel;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeaponConstructor_InitializeWeaponWithParameters_ShouldCreateAnWeaponWithDefaultSlot()
        {
            // Arrange
            var expected = Slot.Weapon;

            //Act 
            var sword = new Weapon("Sword", 1, WeaponType.Swords, 1);
            var actual = sword.Slot;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(WeaponType.Bows)]
        [InlineData(WeaponType.Daggers)]
        [InlineData(WeaponType.Hammers)]
        [InlineData(WeaponType.Axes)]
        [InlineData(WeaponType.Staffs)]
        [InlineData(WeaponType.Wands)]
        [InlineData(WeaponType.Swords)]
        public void WeaponConstructor_InitializeWeaponWithParameters_ShouldCreateAnWeaponWithSpecifiedType(WeaponType type)
        {
            // Arrange
            var expected = type;

            //Act 
            var weapon = new Weapon("weapon", 1, type , 1);
            var actual = weapon.WeaponType;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public void WeaponConstructor_InitializeWeaponWithParameters_ShouldCreateAnWeaponWithSpecifiedDamage(int damage)
        {
            // Arrange
            var expected = 1;

            //Act 
            var sword = new Weapon("Sword", 1, WeaponType.Swords, damage);
            var actual = sword.RequiredLevel;

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion Weapon
    }
}
