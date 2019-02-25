using airtabletodotnet.Lib.DataClasses;
using SSoT.me.AirtableToDotNetLib;
using SSoT.me.AirtableToDotNetLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirtableToDotNet.Tests
{
    public partial class HelloWorldAAW : AirtableAPIWrapper
    {
        /// <summary>
        /// Called before a manufacturere is updated.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="manufacturer">The manufacturer to update</param>
        partial void BeforeUpdateManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Called after a manufacturer is updated
        /// </summary>
        /// <param name="manufacturer">The manufacturer which was updated</param>
        partial void AfterUpdateManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Called before a manufacturere is added.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="manufacturer">The manufacturer to add</param>
        partial void BeforeAddManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Called after a manufacturer is added
        /// </summary>
        /// <param name="manufacturer">The manufacturer which was added</param>
        partial void AfterAddManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Called before a manufacturere is deleted.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="manufacturer">The manufacturer to add</param>
        partial void BeforeDeleteManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Called after a manufacturer is deleted
        /// </summary>
        /// <param name="manufacturer">The manufacturer which was deleted</param>
        partial void AfterDeleteManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Returns a list of manufacturers
        /// </summary>
        /// <param name="view">the specific view to pull manufacturers from</param>
        /// <returns>The list of manufacturers from the given view</returns>
        public IEnumerable<Manufacturer> GetManufacturers(String view)
        {
            var rows = this.GetTableAsAirtableRows("Manufacturers", view);
            return rows.ConvertTo<Manufacturer>();
        }

        /// <summary>
        /// Throw this exception to SKIP a CRUD operation like update, insert or delete
        /// </summary>
        public class SkipOperationException : Exception
        {
            public SkipOperationException()
            {
            }

            public SkipOperationException(string message) : base(message)
            {
            }

            public SkipOperationException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected SkipOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }


        /// <summary>
        /// Update the given manufacturer
        /// </summary>
        /// <param name="manufacturer">The manufacturer to update</param>
        public void Update(Manufacturer manufacturer)
        {
            try
            {
                this.BeforeUpdateManufacturer(manufacturer);
                this.UpdateAirtableRow<Manufacturer>("Manufacturers", manufacturer);
                this.AfterUpdateManufacturer(manufacturer);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Delete the given manufacturer
        /// </summary>
        /// <param name="manufacturer">The manufacturer to delete</param>
        public void DeleteManufacturer(Manufacturer manufacturer)
        {
            try
            {
                this.BeforeDeleteManufacturer(manufacturer);
                this.DeleteAirtableRow<Manufacturer>("Manufacturers", manufacturer);
                this.AfterDeleteManufacturer(manufacturer);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Insert a new manufacturer into the airtable
        /// </summary>
        /// <param name="manufacturer">The manufacturer to insert into the airtable</param>
        /// <returns></returns>
        public Manufacturer InsertManufacturer(Manufacturer manufacturer)
        {
            try
            {
                this.BeforeAddManufacturer(manufacturer);
                manufacturer = base.AddAirtableRow<Manufacturer>("Manufacturers", manufacturer);
                this.AfterAddManufacturer(manufacturer);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }

            return manufacturer;
        }
    }
}
