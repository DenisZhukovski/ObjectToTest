using System;
using System.Threading.Tasks;

namespace ObjectToTest.UnitTests.Models
{
    public class With2FuncArguments
    {
        private readonly Func<int> _getInt;
        private readonly Func<Task> _getTask;

        public With2FuncArguments(Func<int> getInt, Func<Task> getTask)
        {
            _getInt = getInt;
            _getTask = getTask;
        }
    }
}