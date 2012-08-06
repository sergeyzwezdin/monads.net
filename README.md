Monads for .NET
===============

**Monads for .NET** is helpers for C# which makes easier every day of your developer life. Now supports .NET 3.5-4.0, Silverlight 3-5 and Windows Phone 7.

In functional programming, a monad is a programming structure that represents computations. Monads are a kind of abstract data type constructor that encapsulate program logic instead of data in the domain model. A defined monad allows the programmer to chain actions together to build a pipeline to process data in various steps, in which each action is decorated with additional processing rules provided by the monad. Programs written in functional style can make use of monads to structure procedures that include sequenced operations, or to define some arbitrary control flows (like handling concurrency, continuations, side effects such as input/output, or exceptions).

***

Supported platforms
-------------------
1. .NET 3.5-4
2. Silverlight 3-5
3. WP7
4. XNA

Installing
----------
1. Just reference **"Monads.dll"** file and add **"using System.Monads;"** to your code.
2. Install via **nuget**.

Nuget
-----
PM> Install-Package Monads

[Nuget link](http://nuget.org/packages/Monads)

Scenarios
----------
1. Monads for objects
2. Monads for collections 
3. Argument checking 
4. Events 

### Monads for objects

#### Do
Before
<pre>if (data != null)
{
  Console.WriteLine(data);
}</pre>

After
<pre>data.Do(d=>Console.WriteLine(d));</pre>