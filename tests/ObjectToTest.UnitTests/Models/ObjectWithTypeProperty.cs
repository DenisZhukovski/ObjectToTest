using System;

namespace ObjectToTest.UnitTests.Models;

public class ObjectWithTypeProperty
{
    public Type PrefabType => this.GetType();
}