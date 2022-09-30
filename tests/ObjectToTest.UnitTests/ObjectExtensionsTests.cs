using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void HasCircularReference()
        {
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
        
        [Fact]
        public void HasComplexCircularReference()
        {
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