using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;

namespace Tests
{
    public class TestScript
    {
        [Test]
		public void Test()
		{
			string s = "abc";

			Assert.AreEqual("abc", s);
		}
    }
}
