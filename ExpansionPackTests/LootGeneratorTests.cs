using Assignment1.Enums;
using Assignment1.Items;
using ExpansionPack;
using ExpansionPack.Helper;
using ExpansionPack.ItemCreators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ExpansionPackTests
{
    public class LootGeneratorTests
    {
        [Fact]
        public void GetRandomHeadwear_TestUsingAMockedUpArmorCreator_ShouldReturnTheArmor() 
        {
            // arrange
            var standardArmor = new Armor();
            var mockArmor = new Mock<IArmorCreator>();
            mockArmor.Setup(p => p.CreateArmor(It.IsAny<int>(), Slot.Head)).Returns(standardArmor);
            var lootGenerator = new LootGenerator(mockArmor.Object, new RandomWeaponCreator(), new EnumRandomizer(), new ParseRandom());
            var expected = standardArmor;

            // act 
            var actual = lootGenerator.GetRandomHeadwear(1);
            
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRandomBodyPiece_TestUsingAMockedUpArmorCreator_ShouldReturnTheArmor()
        {
            // arrange
            var standardArmor = new Armor();
            var mockArmor = new Mock<IArmorCreator>();
            mockArmor.Setup(p => p.CreateArmor(It.IsAny<int>(), Slot.Body)).Returns(standardArmor);
            var lootGenerator = new LootGenerator(mockArmor.Object, new RandomWeaponCreator(), new EnumRandomizer(), new ParseRandom());
            var expected = standardArmor;

            // act 
            var actual = lootGenerator.GetRandomBodyPiece(1);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRandomBoots_TestUsingAMockedUpArmorCreator_ShouldReturnTheArmor()
        {
            // arrange
            var standardArmor = new Armor();
            var mockArmor = new Mock<IArmorCreator>();
            mockArmor.Setup(p => p.CreateArmor(It.IsAny<int>(), Slot.Legs)).Returns(standardArmor);
            var lootGenerator = new LootGenerator(mockArmor.Object, new RandomWeaponCreator(), new EnumRandomizer(), new ParseRandom());
            var expected = standardArmor;

            // act 
            var actual = lootGenerator.GetRandomBoots(1);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRandomWeapon_TestUsingAMockedUpWeaponCreator_ShouldReturnTheWeapon()
        {
            // arrange
            var standardWeapon = new Weapon();
            var mockWeapon = new Mock<IWeaponCreator>();
            mockWeapon.Setup(p => p.CreateWeapon(It.IsAny<int>())).Returns(standardWeapon);
            var lootGenerator = new LootGenerator(new RandomArmorCreator(), mockWeapon.Object, new EnumRandomizer(), new ParseRandom());
            var expected = standardWeapon;

            // act 
            var actual = lootGenerator.GetRandomWeapon(1);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRandomLoot_TestUsingAMockedUpWeaponCreator_ShouldReturnTheWeapon()
        {
            // arrange
            var standardWeapon = new Weapon();
            var mockWeapon = new Mock<IWeaponCreator>();
            mockWeapon.Setup(p => p.CreateWeapon(It.IsAny<int>())).Returns(standardWeapon);
            var mockEnumRandom = new Mock<IEnumRandomizer>();
            mockEnumRandom.Setup(p => p.RandomEnum(It.IsAny<Enum>())).Returns(Slot.Weapon);
            var lootGenerator = new LootGenerator(new RandomArmorCreator(), mockWeapon.Object, mockEnumRandom.Object, new ParseRandom());
            var expected = standardWeapon;

            // act 
            var actual = lootGenerator.GetRandomLoot(1);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRandomLoot_TestUsingAMockedUpArmorCreator_ShouldReturnTheWeapon()
        {
            // arrange
            var standardArmor = new Armor();
            var mockArmor = new Mock<IArmorCreator>();
            mockArmor.Setup(p => p.CreateArmor(It.IsAny<int>(), Slot.Head)).Returns(standardArmor);
            var mockEnumRandom = new Mock<IEnumRandomizer>();
            mockEnumRandom.Setup(p => p.RandomEnum(It.IsAny<Enum>())).Returns(Slot.Head);
            var lootGenerator = new LootGenerator(mockArmor.Object, new RandomWeaponCreator(), mockEnumRandom.Object, new ParseRandom());
            var expected = standardArmor;

            // act 
            var actual = lootGenerator.GetRandomLoot(1);

            // assert
            Assert.Equal(expected, actual);
        }

    }
}


