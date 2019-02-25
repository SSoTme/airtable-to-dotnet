using airtabletodotnet.Lib.DataClasses;
using SSoT.me.AirtableToDotNetLib;
using SSoT.me.AirtableToDotNetLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirtableToDotNet.Tests
{
    public partial class HelloWorldAAW : AirtableAPIWrapper
    {
        public HelloWorldAAW(string apiKey, string baseId, string baseUrlFormatString = "https://api.airtable.com/v0/{0}/") : base(apiKey, baseId, baseUrlFormatString)
        {

        }

        partial void BeforeUpdateManufacturer(Manufacturer manufacturer)
        {
            manufacturer.CarModels = null;
        }
    }
}
