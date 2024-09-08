## SOLID Principles in C#

“Good software architecture is like a good friend: it supports you, listens to you, and helps you achieve your goals. The SOLID principles provide the blueprint for building these kinds of relationships between your code components.”


The SOLID principles are an acronym for five key design principles: 
  -  Single Responsibility,
  -  Open/Closed, 
  -  Liskov Substitution, 
  -  Interface Segregation, 
  -  Dependency Inversion. 
Let’s dive into each of these principles in detail, along with some examples and tips on how to use them in your C# projects.

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
