using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using airtabletodotnet.Lib.DataClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SSoT.me.AirtableToDotNetLib;
using SSoT.me.AirtableToDotNetLib.Extensions;

namespace AirtableToDotNet.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateWrapper()
        {
            var aaw = new AirtableAPIWrapper(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            Assert.IsNotNull(aaw);
        }

        [TestMethod]
        public void GetSpecificTableAsJson()
        {
            var aaw = new AirtableAPIWrapper(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            var json = aaw.GetTableAsJson("Manufacturers");
            Console.WriteLine(json);
        }

        [TestMethod]
        public void GetSpecificTableAsXml()
        {
            var aaw = new AirtableAPIWrapper(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            var xmlDoc = aaw.GetTableAsXmlDocument("Manufacturers");
            Console.WriteLine(xmlDoc.OuterXml);
        }

        [TestMethod]
        public void GetSpecificTableAsAirtableRows()
        {
            var aaw = new AirtableAPIWrapper(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            IEnumerable<AirtableRow> rows = aaw.GetTableAsAirtableRows("Manufacturers");
            Console.WriteLine(JsonConvert.SerializeObject(rows, Formatting.Indented));

            IEnumerable<Manufacturer> manufacturers = rows.ConvertTo<Manufacturer>();
            Console.WriteLine(JsonConvert.SerializeObject(manufacturers, Formatting.Indented));

            var manuf = manufacturers.First();
            manuf.Total = manuf.Total * manuf.Rate;     
            manuf.LastUnitTest = DateTime.Now;
            manuf.CarModels = null;
            aaw.UpdateAirtableRow("Manufacturers", manuf);
        }

        [TestMethod]
        public void CheckManufacturers()
        {
            var hwaaw = new HelloWorldAAW(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            var manufacturers = hwaaw.GetManufacturers("Foo");
            var honda = manufacturers.FirstOrDefault(fod => fod.Name.Contains("Honda"));
            honda.Notes = "this is atest";
            hwaaw.Update(honda);
        }


        [TestMethod]
        public void GetTableList()
        {

        }
    }
}
