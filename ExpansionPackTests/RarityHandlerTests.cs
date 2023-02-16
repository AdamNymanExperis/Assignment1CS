using Assignment1.Enums;
using ExpansionPack.Enums;
using ExpansionPack.Helper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ExpansionPackTests
{
    public class RarityHandlerTests
    {
        [Theory]
        [InlineData(0, Rarity.Legendary)]
        [InlineData(1, Rarity.Epic)]
        [InlineData(10, Rarity.Epic)]
        [InlineData(11, Rarity.Rare)]
        [InlineData(30, Rarity.Rare)]
        [InlineData(31, Rarity.Common)]
        [InlineData(100, Rarity.Common)]
        public void RandomizeRarity_GivenMockedRandomValues_ShouldSetTheCorrectRarity(int randomNumber, Rarity rarity) 
        {
            //arrange
            var mockRandom = new Mock<IRandom>();
            mockRandom.Setup(p => p.Next(It.IsAny<int>())).Returns(randomNumber);
            var mockEnumRandom = new Mock<IEnumRandomizer>();
            var rarityHandler = new RarityHandler(mockEnumRandom.Object, mockRandom.Object);
            var expected = rarity;

            //act
            rarityHandler.RandomizeRarity();
            var actual = rarityHandler.Rarity;

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, Rarity.Common, CommonPrefix.Common)]
        [InlineData(20, Rarity.Rare, RarePrefix.Rare)]
        [InlineData(10, Rarity.Epic, EpicPrefix.Epic)]
        public void GetRarityPrefix_GivenMockedRandomValues_ShouldReturnTheCorrectPrefix(int randomNumber, Rarity rarity, Enum prefix) 
        {
            // arrange
            var mockRandom = new Mock<IRandom>();
            mockRandom.Setup(p => p.Next(It.IsAny<int>())).Returns(randomNumber);
            
            var mockEnumRandom = new Mock<IEnumRandomizer>();
            mockEnumRandom.Setup(p => p.RandomEnum(It.IsAny<Rarity>())).Returns(rarity);
            mockEnumRandom.Setup(p => p.RandomEnum(It.IsAny<CommonPrefix>())).Returns(prefix);
            mockEnumRandom.Setup(p => p.RandomEnum(It.IsAny<RarePrefix>())).Returns(prefix);
            mockEnumRandom.Setup(p => p.RandomEnum(It.IsAny<EpicPrefix>())).Returns(prefix);

            var rarityHandler = new RarityHandler(mockEnumRandom.Object, mockRandom.Object);
            var expected = rarity.ToString();

            // act
            rarityHandler.RandomizeRarity();
            var actual = rarityHandler.GetRarityPrefix();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRarityPrefix_GivenMockedRandomValuesForLegendary_ShouldReturnTheCorrectPrefix()
        {
            // arrange
            var mockRandom = new Mock<IRandom>();
            mockRandom.Setup(p => p.Next(It.IsAny<int>())).Returns(0);

            var mockEnumRandom = new Mock<IEnumRandomizer>();

            var rarityHandler = new RarityHandler(mockEnumRandom.Object, mockRandom.Object);
            var expected = "Legendary";

            // act
            rarityHandler.RandomizeRarity();
            var actual = rarityHandler.GetRarityPrefix();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100,0)]
        [InlineData(20, 3)]
        [InlineData(10, 5)]
        [InlineData(0, 7)]
        public void GetRarityBonus_GivenMockedRandomValues_ShouldSetTheCorrectRarityBonus(int randomNumber, int bonus)
        {
            //arrange
            var mockRandom = new Mock<IRandom>();
            mockRandom.Setup(p => p.Next(It.IsAny<int>())).Returns(randomNumber);
            var mockEnumRandom = new Mock<IEnumRandomizer>();
            var rarityHandler = new RarityHandler(mockEnumRandom.Object, mockRandom.Object);
            var expected = bonus;

            //act
            rarityHandler.RandomizeRarity();
            var actual = rarityHandler.GetRarityBonus();

            //assert
            Assert.Equal(expected, actual);
        }

    }
}
