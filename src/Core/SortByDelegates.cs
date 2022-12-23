using System;
using System.Collections.Generic;

namespace ObjectToTest
{
    public class SortByDelegates : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            if (x is Delegate)
            {
                if (y is Delegate)
                {
                    return 0;
                }
            
                return 1;
            }
            
            if (y is Delegate)
            {
                return -1;
            }
            
            return 0;
        }
    }
}