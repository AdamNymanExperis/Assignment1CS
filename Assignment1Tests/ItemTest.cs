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
        [Fact]
        public void Construct_WhenInitialized_ShouldCreateANewItemObjectWithNameItem()
        {
            // Arrange
            string expected = "Item";
            var mock = new Mock<Item>();

            //Act 
            var actual = mock.Object.Name;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
