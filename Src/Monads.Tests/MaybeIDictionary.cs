using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Monads.Tests
{
	[TestClass]
	public class MaybeIDictionary
	{
		[TestMethod]
		public void DoWithNotEmpty()
		{
			var source = new Dictionary<int, string> { { 1, "a" }, { 2, "b" }, { 3, "c" } };
			var result = new List<string>();

			source.Do((k, v) => result.Add(k.ToString() + "-" + v));

			Assert.AreEqual(source.Count, result.Count);
			Assert.AreEqual("1-" + source[1], result[0]);
			Assert.AreEqual("2-" + source[2], result[1]);
			Assert.AreEqual("3-" + source[3], result[2]);
		}

		[TestMethod]
		public void DoWithEmpty()
		{
			Dictionary<int, string> source = null;
			var result = new List<string>();

			source.Do((k, v) => result.Add(k.ToString() + "-" + v));

			Assert.AreEqual(0, result.Count);
		}

		[TestMethod]
		public void WithNotEmpty()
		{
			var source = new Dictionary<int, string> { { 1, "a" }, { 2, "b" }, { 3, "c" } };
			var result = source.With(2);

			Assert.AreEqual("b", result);
		}

		[TestMethod]
		public void WithEmpty()
		{
			Dictionary<int, string> source = null;
			var result = source.With(2);

			Assert.AreEqual(null, result);
		}

		[TestMethod]
		public void ReturnNotEmpty()
		{
			var source = new Dictionary<int, string> { { 1, "a" }, { 2, "b" }, { 3, "c" } };
			var result = source.Return(2, "d");

			Assert.AreEqual("b", result);
		}

		[TestMethod]
		public void ReturnNotEmptyNotContains()
		{
			var source = new Dictionary<int, string> { { 1, "a" }, { 2, "b" }, { 3, "c" } };
			var result = source.Return(10, "d");

			Assert.AreEqual("d", result);
		}

		[TestMethod]
		public void ReturnEmpty()
		{
			Dictionary<int, string> source = null;
			var result = source.Return(2, "d");

			Assert.AreEqual("d", result);
		}
	}
}
