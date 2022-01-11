# ObjectToTest
The package is an extension to C# objects that generates set of unit tests depending on an object internal state and public constructors and methods.
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
Let's say there is a class <b>Foo</b> in a project. It would be really nice to have an extension method which will generate a code that will allow to create the same object with the same internal state.

```cs
public void SomeMethod(Foo foo)
{
   var tests = foo.ToXUnit();
   // In tests variable the generated code could be something like this
   new Foo(new Price(10), new User("userName"));
}

```
This generated code can be easily insterted into Unit Test.
