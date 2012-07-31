using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Monads.Tests
{
	[TestClass]
	public class ActionsTests
	{
		internal class ActionMock
		{
			public event Action TestAction;

			public void InvokeAction()
			{
				TestAction.Execute();
			}
		}
		
		internal class ActionTMock<T> 
		{
			public event Action<T> TestAction;

			public void InvokeAction(T arg)
			{
				TestAction.Execute(arg);
			}
		}

		[TestMethod]
		public void ExecuteNotGenericWithNull()
		{
			var eventMock = new ActionMock();
			eventMock.InvokeAction();
		}

		[TestMethod]
		public void ExecuteNotGenericWithNotNull()
		{
			bool executed = false;

			var eventMock = new ActionMock();
			eventMock.TestAction += () => executed = true; 

			eventMock.InvokeAction();

			Assert.IsTrue(executed);
		}
		
		[TestMethod]
		public void ExecuteGenericWithNull()
		{
			var eventMock = new ActionTMock<string>();
			eventMock.InvokeAction("Test");
		}

		[TestMethod]
		public void ExecuteGenericWithNotNull()
		{
			var executed = false;

			var eventMock = new ActionTMock<string>();
			eventMock.TestAction += arg => executed = arg == "Test"; 

			eventMock.InvokeAction("Test");

			Assert.IsTrue(executed);
		}
	}
}
