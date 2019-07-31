using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Configuration;

namespace PCbuilder_ASP.MVC_.Tests
{
    [TestClass]
    public static class TestDBInit
    {
        public const string TestDatabaseName = "TestDatabase";

        public static string TestDatabaseConnectionString = ConfigurationManager.ConnectionStrings[TestDatabaseName].ConnectionString;

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Database.Delete(TestDatabaseName);
        }
    }
}
