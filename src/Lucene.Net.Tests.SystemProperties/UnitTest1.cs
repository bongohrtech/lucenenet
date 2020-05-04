using Lucene.Net.Util;
using NUnit.Framework;
using System;

namespace Lucene.Net.Tests.SystemProperties
{

    [TestFixture]
    public class Tests : LuceneTestCase
    {
        [OneTimeSetUp]
        public void Intitialise()
        {
            //ConfigurationFactory = new MicrosoftConfigurationFactory("appsettings.json");
        }

        [Test]
        public virtual void Test()
        {
        }

        [Test]
        public virtual void Test1()
        {
            Assert.AreEqual(TEST_LOCALE, "fr-FR");
            Assert.Pass();
        }
        [Test]
        public virtual void Test2()
        {
            Assert.AreEqual(ConfigurationSettings.GetProperty("windir"), "C:\\WINDOWS");
            Assert.Pass();
        }
    }
}