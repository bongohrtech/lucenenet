using System;
using Lucene.Net.Codecs;
using Lucene.Net.Configuration;
using Lucene.Net.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LegacyFrameworkUnitTestProject
{
    [TestClass]
    public class UnitTest1 : LuceneTestCase
    {
        [TestInitialize]
        public void Setup()
        {
            //ConfigurationFactory = new MicrosoftConfigurationFactory("appsettings.json");
            try
            {
                // Setup the factories
                ConfigurationSettings.SetConfigFactory(new SystemPropertiesConfigurationSettingsFactory());
            }
            catch (Exception ex)
            {
                // Write the stack trace so we have something to go on if an error occurs here.
                throw new Exception($"An exception occurred during BeforeClass:\n{ex.ToString()}", ex);
            }
        }

        [TestMethod]
        public virtual void LegacyTest()
        {
            Assert.AreEqual(ConfigurationSettingsFactory.GetProperty("windir"), "C:\\WINDOWS");
        }
    }
}
