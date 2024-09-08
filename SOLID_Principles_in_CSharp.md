## SOLID Principles in C#

“Good software architecture is like a good friend: it supports you, listens to you, and helps you achieve your goals. The SOLID principles provide the blueprint for building these kinds of relationships between your code components.”


The SOLID principles are an acronym for five key design principles: 
  -  [Single Responsibility](#s--single-responsibility-principle)
  -  [Open/Closed](#o--openclosed-principle) 
  -  [Liskov Substitution](#l--liskov-substitution-principle)
  -  [Interface Segregation](), 
  -  [Dependency Inversion](). 

#### Let’s dive into each of these principles in detail, along with some examples and tips on how to use them in your C# projects.

### S — Single Responsibility Principle

The Single Responsibility Principle (SRP) states that a class should have only one reason to change. In other words, a class should be responsible for one and only one thing. This helps keep classes focused and makes them easier to maintain and test.

For example, consider a class that is responsible for both user authentication and data retrieval. This violates the SRP because if we need to modify the authentication logic, we would also need to modify the data retrieval logic. Instead, we should create separate classes for authentication and data retrieval, each with its own responsibility.

Here’s an example of how to apply the SRP in C#:
```C#
// Bad: Authentication and data retrieval logic in the same class
public class UserRepository
{
    public bool Authenticate(string username, string password)
    {
        // Authentication logic
    }

    public List<User> GetUsers()
    {
        // Data retrieval logic
    }
}

// Good: Authentication and data retrieval logic in separate classes
public class UserAuthentication
{
    public bool Authenticate(string username, string password)
    {
        // Authentication logic
    }
}

public class UserRepository
{
    public List<User> GetUsers()
    {
        // Data retrieval logic
    }
}
```


### O — Open/Closed Principle

The Open/Closed Principle (OCP) states that a class should be open for extension but closed for modification. In other words, we should be able to add new functionality to a class without changing its existing code.

For example, consider a class that calculates the total cost of an order. If we need to add a new discount for a specific type of customer, we should be able to do so without modifying the existing code.

Here’s an example of how to apply the OCP in C#:The Open/Closed Principle (OCP) states that a class should be open for extension but closed for modification. In other words, we should be able to add new functionality to a class without changing its existing code.

For example, consider a class that calculates the total cost of an order. If we need to add a new discount for a specific type of customer, we should be able to do so without modifying the existing code.

Here’s an example of how to apply the OCP in C#:
```C#
// Bad: Class is not closed for modification
public class Order
{
    public decimal CalculateTotalCost()
    {
        // Calculate total cost
    }

    public decimal CalculateTotalCostWithDiscountForLoyalCustomers()
    {
        // Calculate total cost with discount for loyal customers
    }
}

// Good: Class is closed for modification, but open for extension
public abstract class Order
{
    public decimal CalculateTotalCost()
    {
        // Calculate total cost
    }

    public abstract decimal CalculateDiscount();
}

public class LoyalCustomerOrder : Order
{
    public override decimal CalculateDiscount()
    {
        // Calculate discount for loyal customers
    }
}
```


### L — Liskov Substitution Principle

The Liskov Substitution Principle (LSP) states that objects of a superclass should be replaceable with objects of a subclass without affecting the correctness of the program. In other words, a subclass should be able to substitute its parent class without causing any unexpected behavior.

For example, consider a class that calculates the area of a rectangle. If we create a subclass that calculates the area of a square, it should still work correctly when substituted for the rectangle class.

Here’s an example of how to apply the LSP in C#:

```C#
// Bad: Subclass does not behave like its parent class
public class Rectangle{
  public virtual int Width { get; set; }
  public virtual int Height { get; set; }

  public int CalculateArea()
  {
      return Width * Height;
  }
  
}

public class Square : Rectangle
{
  public override int Width
  {
    get { return base.Width; }
    set { base.Width = value; base.Height = value; }
  }
  public override int Height
  {
      get { return base.Height; }
      set { base.Height = value; base.Width = value; }
  }

}

// Good: Subclass behaves like its parent class
public abstract class Shape
{
  public abstract int CalculateArea();
}

public class Rectangle : Shape
{
  public int Width { get; set; }
  public int Height { get; set; }
  public override int CalculateArea()
  {
      return Width * Height;
  }
}

public class Square : Shape
{
  public int Side { get; set; }
  public override int CalculateArea()
  {
      return Side * Side;
  }
}

```


