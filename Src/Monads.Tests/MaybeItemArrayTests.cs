using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Monads.Tests
{
  [TestClass]
  public class MaybeItemArrayTests
  {
    [TestMethod]
    public void DoOnArraysWithValue()
    {
      var source = new string[] { "One", "Two", "Three" };

      var result = source.At<string>(0);
      Assert.AreEqual("One", result);
    }

    [TestMethod]
    public void DoOnArraysOutOfIndex()
    {
      var source = new string[] { "One", "Two", "Three" };

      var result = source.At<string>(5);

      Assert.AreEqual(null, result);
    }

    [TestMethod]
    public void DoOnEmptyArray()
    {
      var source = new string[] { };

      var result = source.At<string>(0);

      Assert.AreEqual(null, result);
    }


    [TestMethod]
    public void IsNullTrue()
    {
      var source = new string[] { "One", "Two", "Three" };

      var result = source.At<string>(3).IsNull();

      Assert.AreEqual(true, result);
    }
  }
}
