using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Monads.Tests
{
	[TestClass]
	public class MaybeIEnumerableTests
	{
		[TestMethod]
		public void DoWithNotEmpty()
		{
			var source = new[] { "a1", "a2,", "a3" };
			var result = new List<string>();

			source.Do(s => result.Add(s));

			Assert.AreEqual(source.Length, result.Count);
			Assert.AreEqual(source[0], result[0]);
			Assert.AreEqual(source[1], result[1]);
			Assert.AreEqual(source[2], result[2]);
		}

		[TestMethod]
		public void DoWithEmpty()
		{
			string[] source = null;
			var result = new List<string>();

			source.Do(s => result.Add(s));

			Assert.AreEqual(0, result.Count);
		}


		[TestMethod]
		public void DoWithIndexes()
		{
			var source = new[] { new { Property = "First" }, new { Property = "Second" }, new { Property = "Third" } };

			var result = new List<string>();
			source.Do((s, index) => result.Add(index + " - " + s.Property));

			CollectionAssert.AreEqual(new[] { "0 - First", "1 - Second", "2 - Third" }, result);
		}

		[TestMethod]
		public void WithNotEmpty()
		{
			var source = new[] { "a1", "a2,", "a3" };
			var result = source.With((string s) => s + "b").ToArray();

			Assert.AreEqual(source.Count(), result.Count());
			Assert.AreEqual(source[0] + "b", result[0]);
			Assert.AreEqual(source[1] + "b", result[1]);
			Assert.AreEqual(source[2] + "b", result[2]);
		}

		[TestMethod]
		public void WithEmpty()
		{
			string[] source = null;
			var result = source.With(s => s + "b");

			Assert.IsNull(result);
		}
	}
}
