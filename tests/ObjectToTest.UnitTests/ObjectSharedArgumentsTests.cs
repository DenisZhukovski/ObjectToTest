using System;
using ObjectToTest.Arguments;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Extensions;
using ObjectToTest.UnitTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class ObjectSharedArgumentsTests
    {
        private readonly ITestOutputHelper _output;

        public ObjectSharedArgumentsTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void SharedNotNullArgument()
        {
            var user = new User("user name");
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                new Argument(
                    "user",
                    user.GetType(),
                    user, 
                    user.Constructor(new MockArguments())
                ),
                new ObjectSharedArguments(withUser).Argument(user)
            );
        }

        [Fact]
        public void SharedNullArgument()
        {
            User? user = null;
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                "new WithUserArgument(null,new WithUserPublicProperty())",
                withUser.ToTest()
            );
        }

        [Fact]
        public void EmptyStringWhenNoSharedArguments()
        {
            var withUser = new WithUserPublicProperty
            {
                User = new User("user")
            };
            Assert.Empty(
                new ObjectSharedArguments(withUser).ToString()
            );
        }

        [Fact]
        public void SharedArgumentsInit()
        {
            User user = new User("user name");
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}",
                new ObjectSharedArguments(withUser).ToString()
            );
        }
        
        [Fact(Skip = "Need to be fixed as part of #116 puzzle")]
        public void InitSharedAsMethodDelegate()
        {
            /*
            * @todo #116 60m/DEV ObjectSharedArguments can not initialize the target of function delegate.
             * The class should be able to detect that shared argument is delegate and need to initialize its target instead.
            */
            
            var user = new User("user name");
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}",
                new ObjectSharedArguments(
                    new With2FuncArguments(user.Age, user.LoginToAsync)
                ).ToString()
            );
        }
        
        [Fact]
        public void CircularReferenceInitProperties()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.Equal(
                new  NewLineSeparatedString(
                    "var circularRefPublicProperty1 = new CircularRefPublicProperty1();",
                    "var circularRefPublicProperty2 = new CircularRefPublicProperty2();",
                    "circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;",
                    "circularRefPublicProperty2.PropertyName1 = circularRefPublicProperty1;",
                    string.Empty
                ).ToString(),
                new SharedCircularProperties(
                    new ObjectSharedArguments(o1)
                ).ToString().Log(_output)
            );
        }

        [Fact]
        public void SharedArgumentsInitProperties()
        {
            User user = new User("user name");
            var with2PublicProperties = new With2PublicProperties
            {
                User = user,
                UserPublicProperty = new WithUserPublicProperty
                {
                    User = user
                }
            };
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}",
                new ObjectSharedArguments(with2PublicProperties).ToString()
            );
        }

        [Fact]
        public void SingletonNotSharedArgument()
        {
            Assert.Empty(
                new ObjectSharedArguments(new WithSingletonArgument(SingletonClass.Instance)).ToList()
            );
        }

        [Fact]
        public void SameEqualSameHashCode()
        {
            var customHashCode = new WithCustomHashCode("11", 1);
            Assert.NotNull(
                new ObjectSharedArguments(
                    new WithCustomDataExtended(
                        new WithCustomData(customHashCode),
                        customHashCode
                    )
                ).Argument(customHashCode)
            );
        }

        [Fact]
        public void SameEqualButDifferentHashCode()
        {
            var customHashCode = new WithCustomHashCode("11", 1);
            Assert.Null(
                new ObjectSharedArguments(
                        new WithCustomDataExtended(
                            new WithCustomData(customHashCode),
                            customHashCode
                        )
                ).Argument(new WithCustomHashCode("11", 2))
            );
        }
    }
}

