using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ObjectExtensionsTests
    {
        [Fact(Skip = "Need to fix this test")]
        public void HasCircularReference()
        {
            /*
              * @todo #38:60m/DEV Make HasCircularReference test to be green.
              * HasCircularReference always returns false but the method should be able
             * to detect the situation when object has a circular reference in its field or property.
              */
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.True(o1.HasCircularReference());
        }
        
        [Fact]
        public void DoesNotHaveCircularReference()
        {
            Assert.False(new User("user name").HasCircularReference());
        }
        
        [Fact(Skip = "Need to fix this test")]
        public void HasComplexCircularReference()
        {
            /*
              * @todo #38:60m/DEV Make HasComplexCircularReference test to be green.
              * HasCircularReference method should be able to detect the situation when object has a circular reference
              * but not direct. Instead it uses the object that uses the other one which uses the original in its field or property.
              */
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            var o3 = new CircularRefPublicProperty3();
            o1.PropertyName = o2;
            o2.PropertyName3 = o3;
            o3.PropertyName = o1;
            Assert.True(o1.HasCircularReference());
        }
    }
}