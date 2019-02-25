using airtabletodotnet.Lib.DataClasses;


namespace AirtableToDotNet.APIWrapper
{
    /// <summary>
    /// Airtable API Wrapper for the HelloWorld Base
    /// </summary>
    public partial class HelloWorldAirtableAPIWrapper
    {
        public HelloWorldAirtableAPIWrapper(string apiKey, string baseId, string baseUrlFormatString = "https://api.airtable.com/v0/{0}/") : base(apiKey, baseId, baseUrlFormatString)
        {
        }

        partial void BeforeUpdateManufacturer(Manufacturer manufacturer)
        {
            manufacturer.CarModels = null;
        }
    }
}