using Lucene.Net.Configuration;
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
            try
            {
                // Setup the factories
                ConfigurationSettings.SetConfigFactory(new MicrosoftConfigurationSettingsFactory("appsettings.json"));
            }
            catch (Exception ex)
            {
                // Write the stack trace so we have something to go on if an error occurs here.
                throw new Exception($"An exception occurred during BeforeClass:\n{ex.ToString()}", ex);
            }
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
            Assert.AreEqual(ConfigurationSettingsFactory.GetProperty("windir"), "C:\\WINDOWS");
            Assert.Pass();
        }
    }
}