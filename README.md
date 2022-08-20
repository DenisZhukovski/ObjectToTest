# ObjectToTest
The main idea is to implement an extension method for C# objects that generates initialization piece of code depending on an object internal state and public constructors methods and properties. It should let the developer easily recreate an object state and continue using it in unit test environment.
```cs
public class Foo
{
  public Foo(IPrice price, IUser user)
  {
    _price = price;
    _user = user;
  }
  
  public void DoSomething(int argument)
  {
    // Some logic
  }
}

```
Let's say there is a class <b>Foo</b> in a project. It would be really nice to have an extension method which will generate a piece of code that will allow to recreate the same object with the same internal state.

```cs
public void SomeMethod(Foo foo)
{
  /*
   * The result of ToUnit method should be a string that contains the peice of code to recreate
   * foo object from the scatch.
   * new Foo(new Price(10), new User("userName"));
   */
   var tests = foo.ToUnit();
}

```
This generated code can be easily insterted into Unit Test.
