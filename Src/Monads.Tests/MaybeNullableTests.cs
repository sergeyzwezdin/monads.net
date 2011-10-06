using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Monads.Tests
{
	[TestClass]
	public class MaybeNullableTests
	{
		[TestMethod]
		public void DoOnObjectWithValue()
		{
			int? source = 5;

			var result = String.Empty;
			source.Do(s => result = s.ToString());

			Assert.AreEqual("5", result);
		}

		[TestMethod]
		public void DoOnObjectWithNull()
		{
			int? source = null;

			var result = String.Empty;
			source.Do(s => result = s.ToString());

			Assert.AreEqual(String.Empty, result);
		}

		[TestMethod]
		public void WithOnObjectWithValue()
		{
			int? source = 5;

			var result = source.With(s => s.ToString());

			Assert.AreEqual("5", result);
		}

		[TestMethod]
		public void WithOnObjectWithNull()
		{
			int? source = null;

			var result = source.With(s => s.ToString());

			Assert.AreEqual(default(string), result);
		}

		[TestMethod]
		public void ReturnOnObjectWithValue()
		{
			int? source = 5;

			var result = source.Return(s => s.ToString(), "Nothing");

			Assert.AreEqual("5", result);
		}

		[TestMethod]
		public void ReturnOnObjectWithNull()
		{
			int? source = null;

			var result = source.Return(s => s.ToString(), "Nothing");

			Assert.AreEqual("Nothing", result);
		}

		[TestMethod]
		public void IfOnObjectWithValue()
		{
			int? source = 5;

			Assert.AreEqual(source, source.If(s => s.Value > 3));
			Assert.AreEqual(default(int), source.If(s => s.Value > 6));
		}

		[TestMethod]
		public void IfOnObjectWithNull()
		{
			int? source = null;

			Assert.AreEqual(default(int), source.If(s => s.Value > 3));
			Assert.AreEqual(default(int), source.If(s => s.Value > 6));
		}

		[TestMethod]
		public void IfNotOnObjectWithValue()
		{
			int? source = 5;

			Assert.AreEqual(default(int), source.IfNot(s => s.Value > 3));
			Assert.AreEqual(source, source.IfNot(s => s.Value > 6));
		}

		[TestMethod]
		public void IfNotOnObjectWithNull()
		{
			int? source = null;

			Assert.AreEqual(default(int), source.IfNot(s => s.Value > 3));
			Assert.AreEqual(default(int), source.IfNot(s => s.Value > 6));
		}

		[TestMethod]
		public void RecoverOnObjectWithNull()
		{
			int? source = null;

			var result = source.Recover(() => 10);

			Assert.AreEqual(10, result);
		}

		[TestMethod]
		public void TryDoOnObjectWithValueNoException()
		{
			int? source = 5;

			var r = String.Empty;
			var result = source.TryDo(s => r = s.ToString());

			Assert.AreEqual("5", r);
			Assert.AreEqual(source, result.Item1);
			Assert.AreEqual(null, result.Item2);
		}

		[TestMethod]
		public void TryDoOnObjectWithNullNoException()
		{
			int? source = null;

			var r = String.Empty;
			var result = source.TryDo(s => r = s.ToString());

			Assert.AreEqual(String.Empty, r);
			Assert.AreEqual(null, result.Item1);
			Assert.AreEqual(null, result.Item2);
		}

		[TestMethod]
		public void TryDoOnObjectWithException()
		{
			int? source = 1;

			var r = String.Empty;
			var result = source.TryDo(s => r = (100 / (s - 1)).ToString());

			Assert.AreEqual(String.Empty, r);
			Assert.AreEqual(source, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(DivideByZeroException));
		}

		[TestMethod]
		public void TryDoOnObjectWithExceptionImplicitLambda()
		{
			int? source = 1;

			var result = source.TryDo(s => (100 / (s - 1)).ToString(), ex => ex is DivideByZeroException);

			Assert.AreEqual(source, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(DivideByZeroException));
		}

		[TestMethod]
		public void TryDoOnObjectWithExceptionImplicitArray()
		{
			int? source = 1;

			var result = source.TryDo(s => (100 / (s - 1)).ToString(), typeof(DivideByZeroException), typeof(ArgumentException));

			Assert.AreEqual(source, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(DivideByZeroException));
		}

		[TestMethod]
		public void TryDoOnObjectWithExceptionImplicitArrayFails()
		{
			try
			{
				int? source = 1;

				var result = source.TryDo(s => (100 / (s - 1)).ToString(), typeof(OutOfMemoryException), typeof(ArgumentException));

				Assert.Fail("Exception must be thrown.");
			}
			catch (DivideByZeroException)
			{
			}
		}


		[TestMethod]
		public void TryWithOnObjectWithException()
		{
			int? source = 1;

			var result = source.TryWith(s => (100 / (s - 1)).ToString());

			Assert.AreEqual(null, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(DivideByZeroException));
		}

		[TestMethod]
		public void TryWithOnObjectWithExceptionLambda()
		{
			int? source = 1;

			var result = source.TryWith(s => (100 / (s - 1)).ToString(), ex => ex is DivideByZeroException);

			Assert.AreEqual(null, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(DivideByZeroException));
		}

		[TestMethod]
		public void TryWithOnObjectWithExceptionImplicitArray()
		{
			int? source = 1;

			var result = source.TryWith(s => (100 / (s - 1)).ToString(), typeof(DivideByZeroException));

			Assert.AreEqual(null, result.Item1);
			Assert.IsInstanceOfType(result.Item2, typeof(DivideByZeroException));
		}

		[TestMethod]
		public void TryWithOnObjectWithExceptionImplicitArrayFails()
		{
			try
			{
				int? source = 1;

				var result = source.TryWith(s => (100 / (s - 1)).ToString(), typeof(OutOfMemoryException));

				Assert.Fail("Exception must be thrown.");
			}
			catch (DivideByZeroException)
			{
			}
		}

		[TestMethod]
		public void IsNullTrue()
		{
			int? source = null;

			var result = source.IsNull();

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void IsNullFalse()
		{
			int? source = 5;

			var result = source.IsNull();

			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void IsNotNullTrue()
		{
			int? source = 5;

			var result = source.IsNotNull();

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void IsNotNullFalse()
		{
			int? source = null;

			var result = source.IsNotNull();

			Assert.AreEqual(false, result);
		}
	}
}