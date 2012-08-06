Monads for .NET
===============

**Monads for .NET** is helpers for C# which makes easier every day of your developer life. Now supports .NET 3.5-4.0, Silverlight 3-5 and Windows Phone 7.

In functional programming, a monad is a programming structure that represents computations. Monads are a kind of abstract data type constructor that encapsulate program logic instead of data in the domain model. A defined monad allows the programmer to chain actions together to build a pipeline to process data in various steps, in which each action is decorated with additional processing rules provided by the monad. Programs written in functional style can make use of monads to structure procedures that include sequenced operations, or to define some arbitrary control flows (like handling concurrency, continuations, side effects such as input/output, or exceptions).

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

## Scenarios

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

#### With
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

#### Return
Before
<pre>string workPhoneCode = "default code";

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
<pre>string workPhoneCode = person.With(p=>p.Work).With(w=>w.Phone).Return(p=>p.Code, "default code");</pre>

#### If/IfNot
Before
<pre>if ((person.Age > 18) &&
   (person.LastName != null) &&
   (person.Work != null))
{
     Console.WriteLine(person);
}</pre>

After
<pre>person.If(p=>p.Age > 18).IfNot(p=>p.LastName == null).IfNot(p=>p.Work == null).Do(p=>Console.WriteLine(person));</pre>

#### Recover
Before
<pre>var person = new Person();

// do something

if (person == null)
{
  person = new Person();
}

Console.WriteLine(person.LastName);</pre>

After
<pre>var person = new Person();

// do something

Console.WriteLine(person.Recover(()=>new Person()).LastName);</pre>

#### TryDo/Catch
Before
<pre>var person = ... ;
// person.Work = null;
try
{
  // NullReferenceException?
  Console.WriteLine(person.Work.Address);
}
catch
{
}</pre>

After
<pre>var person = ... ;
// person.Work = null;
person.TryDo(p=>Console.WriteLine(p.Work.Address));</pre>

--

Before
<pre>var person = ... ;
// person.Work = null;
try
{
  // NullReferenceException?
  Console.WriteLine(person.Work.Address);
}
catch
{
  Console.WriteLine("Error");
}</pre>

After
<pre>person.TryDo(p=>Console.WriteLine(p.Work.Address)).Catch(e=>Console.WriteLine("Error"));</pre>

#### Checking exception type (via Type)
Before
<pre>var person = ... ;
// person.Work = null;
try
{
  // NullReferenceException?
  Console.WriteLine(person.Work.Address);
}
catch (NullReferenceException)
{
  Console.WriteLine("Error");
}</pre>

After
<pre>person.TryDo(p=>Console.WriteLine(p.Work.Address), typeof(NullReferenceException))
	.Catch(e=>Console.WriteLine("Error"));</pre>

#### Checking exception type (via predicate)
Before
<pre>var person = ... ;
// person.Work = null;
try
{
  // NullReferenceException?
  Console.WriteLine(person.Work.Address);
}
catch (NullReferenceException ex)
{
  if (ex.Message.Contains("some word") == false)
  {
    throw;
  }
  Console.WriteLine("Error");
}</pre>

After
<pre>person.TryDo(p=>Console.WriteLine(p.Work.Address), e=>e.Message.Contains("some words"))
	.Catch(e=>Console.WriteLine("Error"));</pre>

#### Ignore exceptions
Before
<pre>var person = ... ;
// person.Work = null;
try
{
  // NullReferenceException?
  Console.WriteLine(person.Work.Address);
}
catch
{
}</pre>

After
<pre>person.TryDo(p=>Console.WriteLine(p.Work.Address)).Catch();</pre>

#### TryWith/Catch
Before
<pre>var person = ... ;
// person.Work = null;
try
{
  // NullReferenceException?
  var address = person.Work.Address;
  Console.WriteLine (address.TrimLeft());
}
catch
{
}</pre>

After
<pre>var person = ... ;
// person.Work = null;
person.TryWith(p=>p.Work.Address).Catch().Do(p=>Console.WriteLine(p));</pre>

Exception matching and handling policies such as for TryDo.



### Monads for collections 

#### Do
**Enumerable**

Before
<pre>IEnumerable&lt;string&gt; data = ...;
// ...
if (data != null)
{
  foreach (var d in data)
  {
    Console.WriteLine(d);
  }
}</pre>

After
<pre>IEnumerable&lt;string&gt; data = ...;
// ...
data.Do(d=&gt;Console.WriteLine(d));</pre>

**Dictionary**

Before
<pre>IDictionary&lt;int, string&gt; data = ... ;
// ...
if (data != null)
{
  foreach (var d in data)
  {
     Console.WriteLine("{0} - {1}", d.Key, d.Value);
  }
}</pre>

After
<pre>IDictionary data = ... ;
// ...
data.Do((k,v)=&gt;Console.WriteLine("{0} - {1}", k, v));</pre>

#### With
**Enumerable**

Before
<pre>IEnumerable&lt;string&gt; data = ...;
// ...
if (data != null)
{
  var result = data.Select(d=&gt;d.Trim());
}</pre>

After
<pre>IEnumerable&lt;string&gt; data = ...;
// ...
var result = data.With(d=&gt;d.Trim());</pre>

**Dictionary**

Before
<pre>IDictionary&lt;int, string&gt; data = ... ;
// ...
if (data != null)
{
  string result;
  if (data.TryGetValue(1, out result) == true)
  {
    Console.WriteLine(result);
  }
}</pre>

After
<pre>IDictionary&lt;int, string&gt; data = ... ;
// ...
var result = data.With(1);
if (result != null)
{
  Console.WriteLine(result);
}</pre>

#### Return
**Dictionary**

Before
<pre>IDictionary&lt;int, string&gt; data = ... ;
// ...
if (data != null)
{
  string result;
  if (data.TryGetValue(1, out result) == true)
  {
    Console.WriteLine(result);
  }
  else
  {
    Console.WriteLine("Not found");
  }
}</pre>

After
<pre>IDictionary&lt;int, string&gt; data = ... ;
// ...
Console.WriteLine(data.Return(1, "Not found"));</pre>



### Argument checking

#### CheckNull

Before
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
    if (workInfo == null)
    {
        throw new ArgumentNullException("workInfo");
    }

    _workInfo = workInfo;
  }
}</pre>

After
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
     _workInfo = workInfo.CheckNull("workInfo");
  }
}</pre>


--


Before
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
    if (workInfo == null)
    {
        throw new CustomException("Incorrect data");
    }
    _workInfo = workInfo;
  }
}</pre>

After
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
    _workInfo = workInfo.CheckNull(() => new CustomException("Incorrect data"));
  }
}</pre>


#### CheckNullWithDefault

Before
<pre>public class Person
{
  private Work _workInfo;
  public Person(Work workInfo)
  {
    if (workInfo != null)
    {
        _workInfo = workInfo;
    }
    else
    {
        _workInfo = new Work();
    }
  }
}</pre>

After
<pre>public class Person
{
  private Work _workInfo;
  public Person(Work workInfo)
  {
    _workInfo = workInfo.CheckNullWithDefault(new Work());
  }
}</pre>


#### Check

Before
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
    if (workInfo != null)
    {
      if (workInfo.Count > 5)
      {
        _workInfo = workInfo;
      }
      else
      {
        throw new IndexOutOfRangeException();
      }
    }
    else
    {
        throw new ArgumentNullException("workInfo");
    }
  }
}</pre>

After
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
     _workInfo = workInfo.CheckNull("workInfo").Check(s=>s.Count>5, s=>new IndexOutOfRangeException());
  }
}</pre>


#### CheckWithDefault

Before
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
    if (workInfo != null)
    {
      if (workInfo.Count > 5)
      {
        _workInfo = workInfo;
      }
      else
      {
        _workInfo = new Work(10);
      }
    }
    else
    {
        throw new ArgumentNullException("workInfo");
    }
  }
}</pre>

After
<pre>public class Person
{
  private Work _workInfo;

  public Person(Work workInfo)
  {
     _workInfo = workInfo.CheckNull("workInfo").CheckWithDefault(s => s.Count>5, new Work(10));
  }
}</pre>



### Events

#### Events invoking

Before
<pre>public class Person
{
   public event EventHandler Updated;

   public void Update()
   {
      if (Updated != null)
      {
           Updated(this, EventArgs.Empty);
      }
   }
}</pre>

After
<pre>public class Person
{
   public event EventHandler Updated;

   public void Update()
   {
       Updated.Execute(this, EventArgs.Empty);
   }
}</pre>


--


Before
<pre>public class Person
{
   public event EventHandler<CustomEventArgs> Updated;

   public void Update()
   {
      if (Updated != null)
      {
           Updated(this, new CustomEventArgs(...));
      }
   }
}</pre>

After
<pre>public class Person
{
   public event EventHandler<CustomEventArgs> Updated;

   public void Update()
   {
      Updated.Execute(this, new CustomEventArgs(...));
   }
}</pre>
