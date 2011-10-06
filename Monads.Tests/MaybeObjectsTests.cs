using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Monads.Tests
{
	[TestClass]
	public class MaybeObjectsTests
	{
		[TestMethod]
		public void DoOnObjectWithValue()
		{
			var source = new { Property = "Some value" };

			var result = String.Empty;
			source.Property.Do(s => result = s);

			Assert.AreEqual("Some value", result);
		}

		[TestMethod]
		public void DoOnObjectWithNull()
		{
			var source = new { Property = (string)null };

			var result = String.Empty;
			source.Property.Do(s => result = s);

			Assert.AreEqual(String.Empty, result);
		}

		[TestMethod]
		public void WithOnObjectWithValue()
		{
			var source = new { Property1 = new { Property2 = new { Property3 = "Some value" } } };

			var result = source.With(s => s.Property1).With(s => s.Property2).With(s => s.Property3);

			Assert.AreEqual("Some value", result);
		}

		[TestMethod]
		public void WithOnObjectWithNull()
		{
			var source = new { Property1 = new { Property2 = (object)null } };

			var result = source.With(s => s.Property1).With(s => s.Property2);

			Assert.AreEqual(null, result);
		}

		[TestMethod]
		public void ReturnOnObjectWithValue()
		{
			var source = new { Property = "Some value" };

			var result = source.Return(s => s.Property, "Nothing");

			Assert.AreEqual("Some value", result);
		}

		[TestMethod]
		public void ReturnOnObjectWithNull()
		{
			var source = new { Property = "Some value" };
			source = null;

			var result = source.Return(s => s.Property, "Nothing");

			Assert.AreEqual("Nothing", result);
		}

		[TestMethod]
		public void IfOnObjectWithValue()
		{
			var source = new { Property = 5 };

			Assert.AreEqual(source, source.If(s => s.Property > 3));
			Assert.AreEqual(null, source.If(s => s.Property > 6));
		}

		[TestMethod]
		public void IfOnObjectWithNull()
		{
			var source = new { Property = 5 };
			source = null;

			Assert.IsNull(source.If(s => s.Property > 3));
			Assert.IsNull(source.If(s => s.Property > 6));
		}

		[TestMethod]
		public void IfNotOnObjectWithValue()
		{
			var source = new { Property = 5 };

			Assert.AreEqual(null, source.IfNot(s => s.Property > 3));
			Assert.AreEqual(source, source.IfNot(s => s.Property > 6));
		}

		[TestMethod]
		public void IfNotOnObjectWithNull()
		{
			var source = new { Property = 5 };
			source = null;

			Assert.IsNull(source.IfNot(s => s.Property > 3));
			Assert.IsNull(source.IfNot(s => s.Property > 6));
		}

		[TestMethod]
		public void RecoverOnObjectWithNull()
		{
			string source = "Some value";
			source = null;

			var result = source.Recover(() => "Default");

			Assert.AreEqual("Default", result);
		}

		[TestMethod]
		public void OfTypeOnString()
		{
			object source = "Some value";

			string result = source.OfType<string>();

			Assert.AreEqual("Some value", result);
		}

		[TestMethod]
		public void OfTypeOnInt()
		{
			object source = 5;

			int result = source.OfType<int>();

			Assert.AreEqual(5, result);
		}

		[TestMethod]
		public void OfTypeOnIntVsString()
		{
			object source = 5;

			string result = source.OfType<string>();

			Assert.AreEqual(null, result);
		}

		[TestMethod]
		public void TryDoOnObjectWithValueNoException()
		{
			var source = new { Property = "Some value" };

			var r = String.Empty;
			var result = source.TryDo(s => r = s.Property);

			Assert.AreEqual("Some value", r);
			Assert.AreEqual(source, result.Item1);
			Assert.AreEqual(null, result.Item2);
		}

		[TestMethod]
		public void TryDoOnObjectWithNullNoException()
		{
			var source = new { Property = "Some value" };
			source = null;

			var r = String.Empty;
			var result = source.TryDo(s => r = s.Property);

			Assert.AreEqual(String.Empty, r);
			Assert.AreEqual(null, result.Item1);
			Assert.AreEqual(null, result.Item2);
		}

		[TestMethod]
		public void TryDoOnObjectWithException()
		{
			var source = new { Property = (string)null };

			var r = String.Empty;
			var result = source.TryDo(s => r = s.Property.ToString());

			Assert.AreEqual(String.Empty, r);
			Assert.AreEqual(source, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(NullReferenceException));
		}

		[TestMethod]
		public void TryDoOnObjectWithExceptionImplicitLambda()
		{
			var source = new { Property = (string)null };

			var result = source.TryDo(s => s.Property.ToString(), ex => ex is NullReferenceException);

			Assert.AreEqual(source, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(NullReferenceException));
		}

		[TestMethod]
		public void TryDoOnObjectWithExceptionImplicitArray()
		{
			var source = new { Property = (string)null };

			var result = source.TryDo(s => s.Property.ToString(), typeof(NullReferenceException), typeof(ArgumentException));

			Assert.AreEqual(source, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(NullReferenceException));
		}

		[TestMethod]
		public void TryDoOnObjectWithExceptionImplicitArrayFails()
		{
			try
			{
				var source = new { Property = (string)null };

				var result = source.TryDo(s => s.Property.ToString(), typeof(OutOfMemoryException), typeof(ArgumentException));

				Assert.Fail("Exception must be thrown.");
			}
			catch (NullReferenceException)
			{
			}
		}


		[TestMethod]
		public void TryWithOnObjectWithException()
		{
			var source = new { Property1 = (string)null };

			var result = source.TryWith(s => s.Property1.ToString());

			Assert.AreEqual(null, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(NullReferenceException));
		}

		[TestMethod]
		public void TryWithOnObjectWithExceptionLambda()
		{
			var source = new { Property1 = (string)null };

			var result = source.TryWith(s => s.Property1.ToString(), ex => ex is NullReferenceException);

			Assert.AreEqual(null, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(NullReferenceException));
		}

		[TestMethod]
		public void TryWithOnObjectWithExceptionImplicitArray()
		{
			var source = new { Property1 = (string)null };

			var result = source.TryWith(s => s.Property1.ToString(), typeof(NullReferenceException));

			Assert.AreEqual(null, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(NullReferenceException));
		}

		[TestMethod]
		public void TryWithOnObjectWithExceptionImplicitArrayFails()
		{
			try
			{
				var source = new { Property1 = (string)null };

				var result = source.TryWith(s => s.Property1.ToString(), typeof(OutOfMemoryException));

				Assert.Fail("Exception must be thrown.");
			}
			catch (NullReferenceException)
			{
			}
		}

		[TestMethod]
		public void CatchIgnore()
		{
			var source = new Tuple<string, Exception>("Some value", new ArgumentException());

			var result = source.Catch();

			Assert.AreEqual("Some value", result);
		}

		[TestMethod]
		public void CatchLog()
		{
			var source = new Tuple<string, Exception>("Some value", new ArgumentException());

			Exception resultException = null;
			var result = source.Catch(ex => resultException = ex);

			Assert.AreEqual("Some value", result);
			Assert.IsInstanceOfType(resultException, typeof(ArgumentException));
		}

		[TestMethod]
		public void IsNullTrue()
		{
			string source = null;

			var result = source.IsNull();

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void IsNullFalse()
		{
			var source = "Some value";

			var result = source.IsNull();

			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void IsNotNullTrue()
		{
			var source = "Some value";

			var result = source.IsNotNull();

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void IsNotNullFalse()
		{
			string source = null;

			var result = source.IsNotNull();

			Assert.AreEqual(false, result);
		}
	}
}
