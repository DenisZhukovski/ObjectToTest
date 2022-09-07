using System;
namespace ObjectToTest.UnitTests.Models
{
    public class SingletonClass
    {
        private static SingletonClass _instance;

        private SingletonClass()
        {
        }

        public static SingletonClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SingletonClass();
                }

                return _instance;
            }
        }
    }
}
