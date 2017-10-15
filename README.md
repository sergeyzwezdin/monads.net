---

# WARNING: Project depreacted in favor of native C# syntax. Please, don't use it for new projects.

---

Monads for .NET
===============

**Monads for .NET** is helpers for C# which makes easier every day of your developer life. Now supports .NET 3.5-4.0, Silverlight 3-5 and Windows Phone 7.

In functional programming, a monad is a programming structure that represents computations. Monads are a kind of abstract data type constructor that encapsulate program logic instead of data in the domain model. A defined monad allows the programmer to chain actions together to build a pipeline to process data in various steps, in which each action is decorated with additional processing rules provided by the monad. Programs written in functional style can make use of monads to structure procedures that include sequenced operations, or to define some arbitrary control flows (like handling concurrency, continuations, side effects such as input/output, or exceptions).

More information about monads at <a href="http://en.wikipedia.org/wiki/Monad_(functional_programming)">Wikipedia</a>.

***

## Supported platforms

1. .NET 3.5-4
2. Silverlight 3-5
3. WP7
4. XNA

## Installing

1. Just reference **"Monads.dll"** file and add **"using System.Monads;"** to your code.
2. Install via **nuget**.

## Nuget

PM> Install-Package Monads

[Nuget link](http://nuget.org/packages/Monads)

## Contribution

I'm glad to see your contributions for Monads.NET.
Just fork the project and pull request when you're ready.

## Contacts

Feel free to communicate with me by twitter or e-mail:
* e-mail: sergey _ _at_ _ zwezdin _ _dot_ _ com
* twitter: [@sergun](http://twitter.com/sergun)
* blog: [http://zwezdin.com](http://zwezdin.com)

## License
Released under the [MIT license](http://www.opensource.org/licenses/MIT).

## Benefits (code samples)

Before
<pre>string workPhoneCode;

if (person != null)
{
  if (person.Work != null)
  {
    if (person.Work.Phone != null)
    {
       workPhoneCode = person.Work.Phone.Code;
    }
  }
}</pre>

After
<pre>string workPhoneCode = person.With(p=>p.Work).With(w=>w.Phone).With(p=>p.Code);</pre>

---

More info at [wiki](https://github.com/sergun/monads.net/wiki):

1. [Monads for objects](https://github.com/sergun/monads.net/wiki/Monads-for-objects)
2. [Monads for collections](https://github.com/sergun/monads.net/wiki/Monads-for-collections)
3. [Argument checking](https://github.com/sergun/monads.net/wiki/Argument-checking)
4. [Events](https://github.com/sergun/monads.net/wiki/Events)
