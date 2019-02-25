using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SSoT.me.AirtableToDotNetLib
{
    public class AirtableAPIWrapperBase
    {
        private string baseId;
        private string baseUrl;
        private string apiKey;
        private List<string> _tableNames;
        private Dictionary<string, string> _airTables;

        public Dictionary<String, XmlDocument> XmlDocs { get; }
        public WebClient WebClient { get; }
        public StringBuilder XmlBuilder { get; }
        public Dictionary<string, string> AirTables
        {
            get
            {
                if (ReferenceEquals(_airTables, null))
                {
                    this.FindAirTables();
                }
                return _airTables;
            }
            set => _airTables = value;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public DataTable GetTableAsDataTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public XmlDocument GetTableAsXmlDocument(string tableName, String view = "")
        {
            var doc = this.AddAirtableTable(tableName, view);
            if (ReferenceEquals(doc, null))
            {
                doc = new XmlDocument();
                doc.LoadXml("<data></data>");
            }
            return doc;
        }

        public T AddAirtableRow<T>(string tableName, T itemToAdd)
        {
            var id = String.Empty;
            var createdTime = String.Empty;
            var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
            if (!ReferenceEquals(pi, null))
            {
                id = pi.GetValue(itemToAdd).SafeToString();
                pi.SetValue(itemToAdd, null);
            }
            pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                             .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "createdTime", StringComparison.OrdinalIgnoreCase) || wherePI.Name.ToLower().Contains("created"));
            if (!ReferenceEquals(pi, null))
            {
                createdTime = pi.GetValue(itemToAdd).SafeToString();
                pi.SetValue(itemToAdd, null);
            }

            var json = String.Format("{{ \"fields\":{0} }}", JsonConvert.SerializeObject(itemToAdd, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
            var airtableRow = JsonConvert.DeserializeObject<AirtableRow>(json);
            var aritableRowJson = JsonConvert.SerializeObject(airtableRow);
            var tableUrl = String.Format("{0}{1}/{2}", baseUrl, tableName, id);

            this.WebClient.Headers["Authorization"] = String.Format("Bearer {0}", this.apiKey);
            this.WebClient.Headers.Add("Content-Type", "application/json");
            try
            {
                var response = this.WebClient.UploadData(tableUrl, "POST", Encoding.UTF8.GetBytes(json));
            }
            catch (WebException we)
            {
                throw new Exception(new StreamReader(we.Response.GetResponseStream()).ReadToEnd());
            }

            return itemToAdd;
        }

        public void DeleteAirtableRow<T>(string tableName, T itemToDelete)
        {
            var id = String.Empty;
            var createdTime = String.Empty;
            var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
            if (!ReferenceEquals(pi, null))
            {
                id = pi.GetValue(itemToDelete).SafeToString();
                pi.SetValue(itemToDelete, null);
            }

            var tableUrl = String.Format("{0}{1}/{2}", baseUrl, tableName, id);

            this.WebClient.Headers["Authorization"] = String.Format("Bearer {0}", this.apiKey);
            this.WebClient.Headers.Add("Content-Type", "application/json");
            try
            {
                var response = this.WebClient.UploadData(tableUrl, "DELETE", new byte[] { });
            }
            catch (WebException we)
            {
                throw new Exception(new StreamReader(we.Response.GetResponseStream()).ReadToEnd());
            }            
        }

        public string GetTableAsJson(string tableName)
        {
            var doc = this.AddAirtableTable(tableName);
            if (!ReferenceEquals(doc, null))
            {
                return doc.XmlToJson();
            }
            else return "{}";
        }

        public IEnumerable<AirtableRow> GetTableAsAirtableRows(string tableName, String view = "")
        {
            var xmlDoc = this.GetTableAsXmlDocument(tableName, view);
            var nodeList = xmlDoc.SelectNodes("/*/*");
            var sb = new StringBuilder();
            sb.Append("<Airtable>");
            foreach (XmlElement node in nodeList)
            {
                var airtableRow = xmlDoc.CreateElement("AirtableRows");
                airtableRow.InnerXml = node.InnerXml;
                sb.Append(airtableRow.OuterXml);
            }
            sb.Append("</Airtable>");
            var xml = sb.ToString();
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            var json = xdoc.XmlToJson();
            Console.WriteLine(json);
            var airtableTable = JsonConvert.DeserializeObject<AirtableContainer>(json);

            return airtableTable.Airtable.AirtableRows;

        }

        public void UpdateAirtableRow<T>(String tableName, T itemToUpdate)
        {
            var id = String.Empty;
            var createdTime = String.Empty;
            var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
            if (!ReferenceEquals(pi, null))
            {
                id = pi.GetValue(itemToUpdate).SafeToString();
                pi.SetValue(itemToUpdate, null);
            }
            pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                             .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "createdTime", StringComparison.OrdinalIgnoreCase) || wherePI.Name.ToLower().Contains("created"));
            if (!ReferenceEquals(pi, null))
            {
                createdTime = pi.GetValue(itemToUpdate).SafeToString();
                pi.SetValue(itemToUpdate, null);
            }

            var json = String.Format("{{ \"fields\":{0} }}", JsonConvert.SerializeObject(itemToUpdate, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
            var airtableRow = JsonConvert.DeserializeObject<AirtableRow>(json);
            var aritableRowJson = JsonConvert.SerializeObject(airtableRow);
            var tableUrl = String.Format("{0}{1}/{2}", baseUrl, tableName, id);

            this.WebClient.Headers["Authorization"] = String.Format("Bearer {0}", this.apiKey);
            this.WebClient.Headers.Add("Content-Type", "application/json");
            try
            {
                var response = this.WebClient.UploadData(tableUrl, "PATCH", Encoding.UTF8.GetBytes(json));
            }
            catch (WebException we)
            {
                throw new Exception(new StreamReader(we.Response.GetResponseStream()).ReadToEnd());
            }
        }


        /// <summary>
        ///  Creates an Airtable API Wrapper which can easily read, write, update and delete 
        ///  values from an Airtable base.
        /// </summary>
        /// <param name="apiKey">The API Key issued to you by Airtable</param>
        /// <param name="baseId">The Airtable Base ID (which can be retrieved from the API Documentation url)</param>
        /// <param name="baseUrlFormatString">URL to add the base id into. Default value: https://api.airtable.com/v0/{0}/</param>
        public AirtableAPIWrapperBase(string apiKey, string baseId, string baseUrlFormatString = "https://api.airtable.com/v0/{0}/")
        {
            this.apiKey = apiKey;
            this.baseId = baseId;
            this.AirTables = new Dictionary<string, string>();

            Console.WriteLine("Connecting to airtable db...");
            if (String.IsNullOrEmpty(this.baseId)) throw new System.Security.Authentication.AuthenticationException("baseId parameter required");
            if (String.IsNullOrEmpty(this.apiKey)) throw new System.Security.Authentication.AuthenticationException("apiKey parameter required");

            this.XmlDocs = new Dictionary<String, XmlDocument>();
            this.WebClient = new WebClient();

            this.baseUrl = String.Format(baseUrlFormatString, this.baseId);
            this.WebClient.Headers.Add("Authorization", String.Format("Bearer {0}", this.apiKey));
            this.XmlBuilder = new StringBuilder();
        }


        private void FindAirTables()
        {
            _airTables = new Dictionary<string, string>();

            // Get all entities
            try
            {
                this.AddAirtableTable("Entities", "Entity");
            }
            catch
            {
                throw new Exception("Error downloading the Entities (Tables, or TableNames) tab from the specified Airtable base.");
            }

            var entityXmlDoc = this.XmlDocs["Entity"];
            var airtableNameCount = entityXmlDoc.SelectNodes("//fields/AirtableName").Count;

            var entities = entityXmlDoc.SelectNodes("//Entities/Entity");
            foreach (var entity in entities.OfType<XmlElement>())
            {
                var entityName = String.Empty;
                var airtableNameNode = entity.SelectSingleNode("fields/AirtableName");
                var displayNameNode = entity.SelectSingleNode("fields/DisplayName");
                var nameNode = entity.SelectSingleNode("fields/Name");
                var skipAirtableNode = entity.SelectSingleNode("fields/SkipAirtable");
                if (airtableNameCount > 0)
                {
                    if (!ReferenceEquals(airtableNameNode, null))
                    {
                        entityName = airtableNameNode.InnerText;
                    }
                }
                else if (!ReferenceEquals(displayNameNode, null))
                {
                    entityName = displayNameNode.InnerText;
                }
                else if (!ReferenceEquals(nameNode, null))
                {
                    entityName = nameNode.InnerText;
                }

                var entitySingular = entityName;

                if (!ReferenceEquals(nameNode, null)) entitySingular = nameNode.InnerText;


                if (!String.IsNullOrEmpty(entityName))
                {
                    this.AirTables[entitySingular] = entityName;
                }
            }
        }

        public void AddAllTables()
        {
            foreach (string singularName in this.AirTables.Keys)
            {
                var tablename = this.AirTables[singularName];
                try
                {
                    this.AddAirtableTable(tablename, singularName);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Entity {0} not accessible through Airtable API. Either Add a Tab or Delete the AirtableName.", singularName), ex);
                }
            }
        }

        private XmlDocument AddAirtableTable(string tableName, String view = "")
        {
            var singularName = tableName.IsSingular() ? tableName : tableName.Singuluralize();
            var pluralName = tableName.IsPlural() ? tableName : tableName.Pluralize();
            return this.AddAirtableTable(pluralName, singularName, view);
        }
        private XmlDocument AddAirtableTable(string tableName, string singularName, String view)
        {
            Console.WriteLine("Getting airtable {0}", tableName);
            var pluralName = tableName.Replace(" ", "");
            if (pluralName.IsSingular()) pluralName = AirtableExtensions.Pluralize(singularName);

            if (!this.XmlDocs.ContainsKey(singularName))
            {
                var finalXmlDoc = new XmlDocument();
                finalXmlDoc.AppendChild(finalXmlDoc.CreateElement(pluralName));

                var offset = String.Empty;

                while (true)
                {
                    var tableJson = String.Empty;

                    try
                    {
                        var tableUrl = String.Format("{0}{1}?offset={2}", baseUrl, tableName, offset);
                        tableJson = WebClient.DownloadString(tableUrl);
                    }
                    catch (WebException ex2)
                    {
                        var tableUrl = String.Format("{0}{1}?offset={2}&view={3}", baseUrl, HttpUtility.UrlEncode(tableName), offset, view);
                        tableJson = WebClient.DownloadString(tableUrl);
                    }

                    var tableXml = String.Format("{{ \"Recordset\" : {0} }}", tableJson).JsonToXml().OuterXml;
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(tableXml);

                    foreach (var recordElement in xmlDocument.SelectNodes("/*/*").OfType<XmlElement>())
                    {
                        var finalRecord = finalXmlDoc.CreateElement(singularName);
                        finalRecord.InnerXml = recordElement.InnerXml;
                        finalXmlDoc.DocumentElement.AppendChild(finalRecord);
                    }

                    var offsetNode = xmlDocument.SelectSingleNode("/*/offset");
                    if (offsetNode == null)
                    {
                        offset = String.Empty;
                        break;
                    }
                    else
                    {
                        offset = offsetNode.InnerText;
                        Console.WriteLine("Offset: {0}", offset);

                    }
                }

                this.XmlBuilder.AppendLine(finalXmlDoc.DocumentElement.OuterXml);

                this.XmlDocs[singularName] = finalXmlDoc;
            }
            Console.WriteLine("FINISHED - Getting airtable {0}", tableName);
            return this.XmlDocs[singularName];
        }

        private string CleanXML(string airtableXml)
        {
            var xDoc = XDocument.Parse(airtableXml);
            Console.WriteLine("Cleaning elements");
            foreach (XElement element in xDoc.Nodes())
            {
                this.CheckElement(element);
            }

            Console.WriteLine("Cleaning entities");
            foreach (var entity in xDoc.XPathSelectElements("//Entities/Entity"))
            {
                if (entity.XPathSelectElements("Name").ToString() == "") entity.Remove();
            }

            Console.WriteLine("Moving /fields/ up one level.");
            foreach (var fieldValue in xDoc.XPathSelectElements("//fields/*").ToList())
            {
                var parent = fieldValue.Parent.Parent;
                var immediateParent = fieldValue.Parent;
                fieldValue.Remove();
                parent.Add(fieldValue);
                if (!immediateParent.Nodes().Any()) immediateParent.Remove();
            }


            Console.WriteLine("Cleaning text");
            foreach (XText text in xDoc.DescendantNodes().OfType<XText>())
            {
                if (text.Value.Contains("&")) text.Value = String.Format("<![CDATA[{0}]]>", text.Value);
            }

            Console.WriteLine("Cleaning ids");
            var entityDict = new Dictionary<String, XElement>();
            foreach (var id in xDoc.XPathSelectElements("//id"))
            {
                var airtableName = id.Parent.Name.LocalName;
                var entityName = airtableName;
                if (!entityDict.ContainsKey(entityName))
                {
                    entityDict[entityName] = xDoc.XPathSelectElement(String.Format("//Entities/Entity/fields[Name = '{0}' or translate(DisplayName, ' ', '') = '{0}' or DisplayName = '{0}']/Name", airtableName));
                }
                if (!ReferenceEquals(entityDict[entityName], null)) entityName = entityDict[entityName].Value;
                id.Name = String.Format("{0}Id", entityName);
            }

            Console.WriteLine("Recovering cleaned");
            var xml = xDoc.ToString();
            xml = xml.Replace("&lt;airtable:", "&lt;airtable_");
            xml = xml.Replace("&lt;/airtable:", "&lt;/airtable_");
            return xml;
        }

        private void CheckElement(XElement element)
        {
            var name = XmlConvert.DecodeName(element.Name.ToString());
            var cleanName = String.Join(String.Empty, name.Where(whereChar => Char.IsLetterOrDigit(whereChar)));

            if (cleanName != element.Name.ToString())
            {
                element.Name = cleanName;
            }
            foreach (var child in element.Nodes())
            {
                var childElement = child as XElement;
                if (!ReferenceEquals(childElement, null))
                {
                    CheckElement(childElement);
                }
            }
        }
    }

    public class AirtableRow
    {
        public String id { get; set; }
        public Dictionary<String, Object> fields { get; set; }
        public DateTime createdTime { get; set; }
    }

    public class AirtableTable
    {
        public List<AirtableRow> AirtableRows { get; set; }
    }

    public class AirtableContainer
    {
        public AirtableTable Airtable { get; set; }
    }
}
