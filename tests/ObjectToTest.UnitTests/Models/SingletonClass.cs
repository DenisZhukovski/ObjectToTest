using System;
namespace ObjectToTest.UnitTests.Models
{
    public class SingletonClass
    {
        private static SingletonClass? _instance;

        private SingletonClass()
        {
        }

        public static SingletonClass Instance => _instance ??= new SingletonClass();
    }
}
