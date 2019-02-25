using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using airtabletodotnet.Lib.DataClasses;
using AirtableToDotNet.APIWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SSoT.me.AirtableToDotNetLib;

namespace AirtableToDotNet.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateWrapper()
        {
            var aaw = new AirtableAPIWrapperBase(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            Assert.IsNotNull(aaw);
        }

        [TestMethod]
        public void GetSpecificTableAsJson()
        {
            var aaw = new AirtableAPIWrapperBase(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            var json = aaw.GetTableAsJson("Manufacturers");
            Console.WriteLine(json);
        }

        [TestMethod]
        public void GetSpecificTableAsXml()
        {
            var aaw = new AirtableAPIWrapperBase(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            var xmlDoc = aaw.GetTableAsXmlDocument("Manufacturers");
            Console.WriteLine(xmlDoc.OuterXml);
        }

        [TestMethod]
        public void GetSpecificTableAsAirtableRows()
        {
            var aaw = new AirtableAPIWrapperBase(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
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
            var hwaaw = new HelloWorldAirtableAPIWrapper(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            var manufacturers = hwaaw.GetManufacturers("Foo");
            var honda = manufacturers.FirstOrDefault(fod => fod.Name.Contains("Honda"));
            honda.Notes = "this is a test";
            honda.PresidentName = "CEO Bob";
            hwaaw.Update(honda);


            var manufsToDelete = manufacturers.Where(whereManuf => whereManuf.ToDelete.HasValue && whereManuf.ToDelete.Value);
            manufsToDelete.ToList().ForEach(feManufToDelete => hwaaw.Delete(feManufToDelete));
        }


        [TestMethod]
        public void GetTableList()
        {

        }
    }
}
