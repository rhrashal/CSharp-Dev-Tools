## SOLID Principles in C#

“Good software architecture is like a good friend: it supports you, listens to you, and helps you achieve your goals. The SOLID principles provide the blueprint for building these kinds of relationships between your code components.”


The SOLID principles are an acronym for five key design principles: 
  -  Single Responsibility,
  -  Open/Closed, 
  -  Liskov Substitution, 
  -  Interface Segregation, 
  -  Dependency Inversion. 
Let’s dive into each of these principles in detail, along with some examples and tips on how to use them in your C# projects.

# S — Single Responsibility Principle

The Single Responsibility Principle (SRP) states that a class should have only one reason to change. In other words, a class should be responsible for one and only one thing. This helps keep classes focused and makes them easier to maintain and test.

For example, consider a class that is responsible for both user authentication and data retrieval. This violates the SRP because if we need to modify the authentication logic, we would also need to modify the data retrieval logic. Instead, we should create separate classes for authentication and data retrieval, each with its own responsibility.

Here’s an example of how to apply the SRP in C#:
