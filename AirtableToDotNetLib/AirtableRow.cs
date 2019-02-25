using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoT.me.AirtableToDotNetLib
{
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
