﻿using System;
using System.Net.Http;
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
                new ObjectSharedArguments(withUser, true).Argument(user)
            );
        }

        [Fact]
        public void SharedNullArgument()
        {
            User? user = null;
            Assert.Equal(
                "new WithUserArgument(null,new WithUserPublicProperty())",
                new WithUserArgument(
                    user,
                    new WithUserPublicProperty
                    {
                        User = user
                    }
                ).ToTest(_output, false)
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
                new ObjectSharedArguments(withUser, true).ToString()
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
                new ObjectSharedArguments(withUser, true).ToString()
            );
        }
        
        [Fact]
        public void InitSharedAsMethodDelegate()
        {
            var user = new User("user name");
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}",
                new ObjectSharedArguments(
                    new With2FuncArguments(user.Age, user.LoginToAsync),
                    true
                ).ToString(_output)
            );
        }
        
        [Fact]
        public void SharedDelegatesCount()
        {
            var user = new User("user name");
            Assert.Equal(
                3,
                new ObjectSharedArguments(
                    new With2FuncArguments(user.Age, user.LoginToAsync),
                    true
                ).ToList().Count
            );
        }
        
        [Fact]
        public void SharedDelegatesFindsTheObject()
        {
            var user = new User("user name");
            Assert.NotNull(
                new ObjectSharedArguments(
                    new With2FuncArguments(user.Age, user.LoginToAsync),
                    true
                ).Argument(new Func<int>(user.Age))
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
                    new ObjectSharedArguments(o1, true)
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
                new ObjectSharedArguments(with2PublicProperties, true).ToString()
            );
        }

        [Fact]
        public void SingletonNotSharedArgument()
        {
            Assert.Empty(
                new ObjectSharedArguments(
                    new WithSingletonArgument(SingletonClass.Instance),
                    true
                ).ToList()
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
                    ),
                    true
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
                        ),
                        true
                ).Argument(new WithCustomHashCode("11", 2))
            );
        }

        [Fact]
        public void HttpClientNoSharedArguments()
        {
            Assert.Empty(
                new ObjectSharedArguments(
                    new HttpClient(),
                    false
                ).ToList()
            );
        }
    }
}

