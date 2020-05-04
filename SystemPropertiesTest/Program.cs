using System;
using Lucene.Net.Util;
using Microsoft.Extensions.Configuration;

namespace SystemPropertiesTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
  //          IConfiguration Configuration = new ConfigurationBuilder()
  //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  //.AddEnvironmentVariables()
  //.AddCommandLine(args)
  //.Build();

            string version = SystemProperties.GetProperty("windir", null);

            //var windir = Configuration["windir"];
            //var luceneversion = Configuration["lucene.version"];
            //var myKeyValue = Configuration["MyKey"];
            //var title = Configuration["Position:Title"];
            //var name = Configuration["Position:Name"];
            //var defaultLogLevel = Configuration["Logging:LogLevel:Default"];


            //string testLocale = Configuration["tests:locale"];
            //string testTimeZone = Configuration["tests:timezone"];

            TestSystemProperties t = new TestSystemProperties(args);

            Lucene.Net.Util.LuceneTestCase.IConfigurationSettings config = new Lucene.Net.Util.LuceneTestCase.MicrosoftConfigurationSettings("appsettings.json");

            Console.WriteLine($"lucene:version: {config.GetProperty("lucene:version") } \n");
            Console.WriteLine($"windir: {config.GetProperty("windir") } \n");
            //Console.WriteLine($"MyKey value: {myKeyValue} \n" +
            //               $"Title: {title} \n" +
            //               $"Name: {name} \n" +
            //               $"windir: {windir} \n" +
            //               $"testLocale: {testLocale} \n" +
            //               $"testTimeZone: {testTimeZone} \n" +
            //               $"lucene.version: {luceneversion} \n" +
            //               $"Default Log Level: {defaultLogLevel}");

        }

    }

    public class TestSystemProperties : LuceneTestCase
    {
        public TestSystemProperties()
        {
            Console.WriteLine($"TEST_LOCALE: {TEST_LOCALE} \n");

        }
        public TestSystemProperties(string[] args) : base(args)
        {
            Console.WriteLine($"TEST_LOCALE: {TEST_LOCALE} \n");

        }
        public TestSystemProperties(IConfiguration config) : base(config)
        {
            Console.WriteLine($"TEST_LOCALE: {TEST_LOCALE} \n");
        }
    }


}