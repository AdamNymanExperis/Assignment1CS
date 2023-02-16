using Assignment1.Enums;
using ExpansionPack.Helper;
using Moq;
using System.Runtime.CompilerServices;

namespace ExpansionPackTests
{
    public class EnumRandomizerTests
    {
        [Theory]
        [InlineData(0, Slot.Weapon)]
        [InlineData(1,Slot.Head)]
        [InlineData(2,Slot.Body)]
        [InlineData(3, Slot.Legs)]
        [InlineData(0, ArmorType.Cloth)]
        [InlineData(1, ArmorType.Leather)]
        [InlineData(2, ArmorType.Mail)]
        [InlineData(3, ArmorType.Plate)]
        [InlineData(0, WeaponType.Axes)]
        [InlineData(1, WeaponType.Bows)]
        [InlineData(2, WeaponType.Daggers)]
        [InlineData(3, WeaponType.Hammers)]
        [InlineData(4, WeaponType.Staffs)] 
        [InlineData(5, WeaponType.Swords)]
        [InlineData(6, WeaponType.Wands)]
        public void RandomEnum_WhenCalledWithSlotsEnum_ShouldReturnTheRightSlotDependingOnInputedNumber(int RandomOutput, Enum theEnum)
        {
            //arrange
            var mockRandom = new Mock<IRandom>();
            mockRandom.Setup(p => p.Next(It.IsAny<int>())).Returns(RandomOutput);
            var enumRandomizer = new EnumRandomizer(mockRandom.Object);
            var expected = theEnum; 
            //act
            var actual = enumRandomizer.RandomEnum(theEnum);
            //assert
            Assert.Equal(expected, actual);
        }
    }
}