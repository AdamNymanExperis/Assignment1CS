using Assignment1.Helper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1Tests
{
    public class HeroAttributeTest
    {
        [Fact]
        public void plusOperator_WhenUsed_ShouldAddTheAttributesOfTheTwoObjectsTogether()
        {
            // Arrange
            HeroAttribute attributes = new HeroAttribute(3,5,3);
            HeroAttribute attributesWithValues = new HeroAttribute(1,2,3);
            var expected = new HeroAttribute(4,7,6);

            //Act 
            var actual = attributes + attributesWithValues;
            

            // Assert
            Assert.True(expected.Equals(actual));
        }
    }
}
