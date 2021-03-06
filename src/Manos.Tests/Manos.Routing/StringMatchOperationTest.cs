
using System;
using NUnit.Framework;

using Manos.Routing;
using System.Collections.Specialized;
using Manos.ShouldExt;
using Manos.Collections;


namespace Manos.Routing.Tests
{


	[TestFixture()]
	public class StringMatchOperationTest
	{

		[Test()]
		public void TestCtor ()
		{
			Should.Throw<ArgumentNullException> (() => new StringMatchOperation (null), "a1");
			Should.Throw<ArgumentException> (() => new StringMatchOperation (String.Empty), "a2");
			
			var op = new StringMatchOperation ("foo");
			Assert.AreEqual ("foo", op.String, "a3");
		}
		
		[Test()]
		public void TestStringProperty ()
		{
			var op = new StringMatchOperation ("foo");
			Assert.AreEqual ("foo", op.String, "a1");
			
			Should.Throw<ArgumentNullException> (() => op.String = null, "a2");
			Should.Throw<ArgumentException> (() => op.String = String.Empty, "a3");
			
			op.String = "baz";
			Assert.AreEqual ("baz", op.String);
		}
		
		[Test()]
		public void MatchFullStringTest ()
		{
			var op = new StringMatchOperation ("foobar");
			var data = new DataDictionary ();
			int end;
			
			bool m = op.IsMatch ("foobar", 0, data, out end);
			
			Assert.IsTrue (m, "a1");
			Assert.AreEqual (0, data.Count, "a2");
			Assert.AreEqual (6, end, "a3");
		}
		
		[Test()]
		public void MatchPartialStringTest ()
		{
			var op = new StringMatchOperation ("foo");
			var data = new DataDictionary ();
			int end;
			
			bool m = op.IsMatch ("foobar", 0, data, out end);
			
			Assert.IsTrue (m, "a1");
			Assert.AreEqual (0, data.Count, "a2");
			Assert.AreEqual (3, end, "a3");
			
			op = new StringMatchOperation ("bar");
			m = op.IsMatch ("foobar", end, data, out end);
			Assert.IsTrue (m, "a4");
			Assert.AreEqual (0, data.Count, "a5");
			Assert.AreEqual (6, end, "a3");
			
		}
		
		[Test ()]
		public void MatchNotAtStartShouldFail ()
		{
			var op = new StringMatchOperation ("oobar");
			var data = new DataDictionary ();
			int end;
			
			bool m = op.IsMatch ("foobar", 0, data, out end);
			
			Assert.IsFalse (m, "a1");
			Assert.AreEqual (0, data.Count, "a2");
			Assert.AreEqual (0, end, "a3");
		}
	}
}
