# ObjectToTest

<h3 align="center">
   
  [![Stars](https://img.shields.io/github/stars/DenisZhukovski/ObjectToTest?color=brightgreen)](https://github.com/DenisZhukovski/ObjectToTest/stargazers) 
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) 
  [![Hits-of-Code](https://hitsofcode.com/github/deniszhukovski/ObjectToTest)](https://hitsofcode.com/view/github/deniszhukovski/ObjectToTest)
  [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_ObjectToTest&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=DenisZhukovski_ObjectToTest)
  [![EO principles respected here](https://www.elegantobjects.org/badge.svg)](https://www.elegantobjects.org)
  [![PDD status](https://www.0pdd.com/svg?name=DenisZhukovski/ObjectToTest)](https://www.0pdd.com/p?name=DenisZhukovski/ObjectToTest)
</h3>

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
   var tests = foo.ToTest();
}

```
This generated code can be easily insterted into Unit Test.

## Build status

<div align="center">
  
   [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_ObjectToTest&metric=alert_status)](https://sonarcloud.io/dashboard?id=DenisZhukovski_ObjectToTest) 
   [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_ObjectToTest&metric=coverage)](https://sonarcloud.io/dashboard?id=DenisZhukovski_ObjectToTest)
   [![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_ObjectToTest&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=DenisZhukovski_ObjectToTest)
   [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_ObjectToTest&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=DenisZhukovski_ObjectToTest) 
</div>
