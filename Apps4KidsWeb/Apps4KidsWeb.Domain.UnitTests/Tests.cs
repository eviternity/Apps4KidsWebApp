using System;
using Apps4KidsWeb.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;

namespace Apps4KidsWeb.Domain.UnitTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestRandomAuthentificationCode()
        {
            string code = (string)typeof(Facade)
                .GetMethod("GenerateAuthenticationCode", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, null);

            Assert.IsTrue(code.Length == 30);
        }
    }
}
