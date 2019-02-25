using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SSoT.me.AirtableToDotNetLib.Extensions
{
    /// <summary>
    /// Helper methods to interact with airtable
    /// </summary>
    public static class AirtableExtensions
    {

        public static IEnumerable<T> ConvertTo<T>(this IEnumerable<AirtableRow> sourceItems)
            where T : class, new()
        {
            var destItems = new List<T>();
            foreach (var sourceItem in sourceItems)
            {
                var itemJson = JsonConvert.SerializeObject(sourceItem.fields);
                var destItem = JsonConvert.DeserializeObject<T>(itemJson);
                destItems.Add(destItem);
                var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
                if (!ReferenceEquals(pi, null)) pi.SetValue(destItem, sourceItem.id);
                pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "createdTime", StringComparison.OrdinalIgnoreCase) || wherePI.Name.ToLower().Contains("created"));
                if (!ReferenceEquals(pi, null)) pi.SetValue(destItem, sourceItem.createdTime);
            }
            return destItems;
        }
        /// <summary>
        /// Convert an xml document into json
        /// </summary>
        /// <param name="doc">The xml document to convert</param>
        /// <returns>A json string converted by Newtonsoft</returns>
        public static String XmlToJson(this XmlDocument doc)
        {
            return JsonConvert.SerializeXmlNode(doc.DocumentElement, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Convert a json string into an Xml Document
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static XmlDocument JsonToXml(this String json)
        {
            try
            {
                var serializedJson = json;
                return JsonConvert.DeserializeXmlNode(serializedJson);
            }
            catch
            {
                json = String.Format("{{ RootNode: {0} }}", json);
                return JsonConvert.DeserializeXmlNode(json);
            }
        }

        /// <summary>
        /// Turn any string or object into a string, even if it's null to start with
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String SafeToString(this object value)
        {
            if (ReferenceEquals(value, null)) return String.Empty;
            else return value.ToString();
        }

        /// <summary>
        /// Convert a string to it's proper (title) case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToPropperCase(this String value)
        {
            value = value.SafeToString();
            if (value.Length <= 1) return value.ToUpper();
            else return String.Join("", value.Substring(0, 1).ToUpper(), value.Substring(1));
        }


        /// <summary>
        /// Pluralize the word provided
        /// </summary>
        /// <param name="singlularText">The singulra text to pluralize</param>
        /// <returns>The pluralized version of the word provided</returns>
        public static String Pluralize(this String singlularText)
        {
            if (String.IsNullOrEmpty(singlularText)) return String.Empty;
            var pluralizer = PluralizationService.CreateService(CultureInfo.CurrentCulture);
            return pluralizer.Pluralize(singlularText.SafeToString());
        }

        /// <summary>
        /// Return true if the text provided is singular
        /// </summary>
        /// <param name="singularCandidate">Singular candidate</param>
        /// <returns>Returns true if the candidate word provided is singular</returns>
        public static bool IsSingular(this String singularCandidate)
        {
            if (String.IsNullOrEmpty(singularCandidate)) return false;
            var pluralizer = PluralizationService.CreateService(CultureInfo.CurrentCulture);
            return pluralizer.IsSingular(singularCandidate.SafeToString());
        }

        /// <summary>
        /// Returns true if the text provided is pluarl
        /// </summary>
        /// <param name="pluralCandidate">The candidate word which might be plural</param>
        /// <returns>Returns true if the candidate word provided is plural</returns>
        public static bool IsPlural(this String pluralCandidate)
        {
            return !pluralCandidate.IsSingular();
        }

        /// <summary>
        /// Singularize the word provided
        /// </summary>
        /// <param name="singlularText">The plural text to Singularize </param>
        /// <returns>The singularized version of the word provided</returns>
        public static String Singuluralize(this String pluralText)
        {
            if (String.IsNullOrEmpty(pluralText)) return String.Empty;
            var pluralizer = PluralizationService.CreateService(CultureInfo.CurrentCulture);
            return pluralizer.Singularize(pluralText.SafeToString());
        }
    }
}
