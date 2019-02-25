using airtabletodotnet.Lib.DataClasses;
using SSoT.me.AirtableToDotNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirtableToDotNet.Tests
{
    public partial class HelloWorldAirtableAPIWrapper : AirtableAPIWrapperBase
    {
        
        /// <summary>
        /// Called before a modelyear is updated.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="modelyear">The modelyear to update</param>
        partial void BeforeUpdateModelYear(ModelYear modelyear);

        /// <summary>
        /// Called after a modelyear is updated
        /// </summary>
        /// <param name="modelyear">The modelyear which was updated</param>
        partial void AfterUpdateModelYear(ModelYear modelyear);

        /// <summary>
        /// Called before a modelyear is added.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="modelyear">The modelyear to add</param>
        partial void BeforeAddModelYear(ModelYear modelyear);

        /// <summary>
        /// Called after a modelyear is added
        /// </summary>
        /// <param name="modelyear">The modelyear which was added</param>
        partial void AfterAddModelYear(ModelYear modelyear);

        /// <summary>
        /// Called before a modelyear is deleted.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="modelyear">The modelyear to add</param>
        partial void BeforeDeleteModelYear(ModelYear modelyear);

        /// <summary>
        /// Called after a modelyear is deleted
        /// </summary>
        /// <param name="modelyear">The modelyear which was deleted</param>
        partial void AfterDeleteModelYear(ModelYear modelyear);
        /// <summary>
        /// Called before a manufacturer is updated.  Throw a SkipOperationException 
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
        /// Called before a manufacturer is added.  Throw a SkipOperationException 
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
        /// Called before a manufacturer is deleted.  Throw a SkipOperationException 
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
        /// Called before a reseller is updated.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="reseller">The reseller to update</param>
        partial void BeforeUpdateReseller(Reseller reseller);

        /// <summary>
        /// Called after a reseller is updated
        /// </summary>
        /// <param name="reseller">The reseller which was updated</param>
        partial void AfterUpdateReseller(Reseller reseller);

        /// <summary>
        /// Called before a reseller is added.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="reseller">The reseller to add</param>
        partial void BeforeAddReseller(Reseller reseller);

        /// <summary>
        /// Called after a reseller is added
        /// </summary>
        /// <param name="reseller">The reseller which was added</param>
        partial void AfterAddReseller(Reseller reseller);

        /// <summary>
        /// Called before a reseller is deleted.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="reseller">The reseller to add</param>
        partial void BeforeDeleteReseller(Reseller reseller);

        /// <summary>
        /// Called after a reseller is deleted
        /// </summary>
        /// <param name="reseller">The reseller which was deleted</param>
        partial void AfterDeleteReseller(Reseller reseller);
        /// <summary>
        /// Called before a carmodel is updated.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="carmodel">The carmodel to update</param>
        partial void BeforeUpdateCarModel(CarModel carmodel);

        /// <summary>
        /// Called after a carmodel is updated
        /// </summary>
        /// <param name="carmodel">The carmodel which was updated</param>
        partial void AfterUpdateCarModel(CarModel carmodel);

        /// <summary>
        /// Called before a carmodel is added.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="carmodel">The carmodel to add</param>
        partial void BeforeAddCarModel(CarModel carmodel);

        /// <summary>
        /// Called after a carmodel is added
        /// </summary>
        /// <param name="carmodel">The carmodel which was added</param>
        partial void AfterAddCarModel(CarModel carmodel);

        /// <summary>
        /// Called before a carmodel is deleted.  Throw a SkipOperationException 
        /// if the update should not happen
        /// </summary>
        /// <param name="carmodel">The carmodel to add</param>
        partial void BeforeDeleteCarModel(CarModel carmodel);

        /// <summary>
        /// Called after a carmodel is deleted
        /// </summary>
        /// <param name="carmodel">The carmodel which was deleted</param>
        partial void AfterDeleteCarModel(CarModel carmodel);

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
        /// Returns a list of ModelYears
        /// </summary>
        /// <param name="view">the specific view to pull ModelYears from</param>
        /// <returns>The list of ModelYears from the given view</returns>
        public IEnumerable<ModelYear> GetModelYears(String view)
        {
            var rows = this.GetTableAsAirtableRows("ModelYears", view);
            return rows.ConvertTo<ModelYear>();
        }

        /// <summary>
        /// Update the given modelyear
        /// </summary>
        /// <param name="modelyear">The modelyear to update</param>
        public void Update(ModelYear modelyear)
        {
            try
            {
                this.BeforeUpdateModelYear(modelyear);
                this.UpdateAirtableRow<ModelYear>("ModelYears", modelyear);
                this.AfterUpdateModelYear(modelyear);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Delete the given modelyear
        /// </summary>
        /// <param name="modelyear">The modelyear to delete</param>
        public void Delete(ModelYear modelyear)
        {
            try
            {
                this.BeforeDeleteModelYear(modelyear);
                this.DeleteAirtableRow<ModelYear>("ModelYears", modelyear);
                this.AfterDeleteModelYear(modelyear);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Insert a new modelyear into the airtable
        /// </summary>
        /// <param name="modelyear">The modelyear to insert into the airtable</param>
        /// <returns></returns>
        public ModelYear Insert(ModelYear modelyear)
        {
            try
            {
                this.BeforeAddModelYear(modelyear);
                modelyear = base.AddAirtableRow<ModelYear>("ModelYears", modelyear);
                this.AfterAddModelYear(modelyear);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }

            return modelyear;
        }
        /// <summary>
        /// Returns a list of Manufacturers
        /// </summary>
        /// <param name="view">the specific view to pull Manufacturers from</param>
        /// <returns>The list of Manufacturers from the given view</returns>
        public IEnumerable<Manufacturer> GetManufacturers(String view)
        {
            var rows = this.GetTableAsAirtableRows("Manufacturers", view);
            return rows.ConvertTo<Manufacturer>();
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
        public void Delete(Manufacturer manufacturer)
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
        public Manufacturer Insert(Manufacturer manufacturer)
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
        /// <summary>
        /// Returns a list of Resellers
        /// </summary>
        /// <param name="view">the specific view to pull Resellers from</param>
        /// <returns>The list of Resellers from the given view</returns>
        public IEnumerable<Reseller> GetResellers(String view)
        {
            var rows = this.GetTableAsAirtableRows("Resellers", view);
            return rows.ConvertTo<Reseller>();
        }

        /// <summary>
        /// Update the given reseller
        /// </summary>
        /// <param name="reseller">The reseller to update</param>
        public void Update(Reseller reseller)
        {
            try
            {
                this.BeforeUpdateReseller(reseller);
                this.UpdateAirtableRow<Reseller>("Resellers", reseller);
                this.AfterUpdateReseller(reseller);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Delete the given reseller
        /// </summary>
        /// <param name="reseller">The reseller to delete</param>
        public void Delete(Reseller reseller)
        {
            try
            {
                this.BeforeDeleteReseller(reseller);
                this.DeleteAirtableRow<Reseller>("Resellers", reseller);
                this.AfterDeleteReseller(reseller);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Insert a new reseller into the airtable
        /// </summary>
        /// <param name="reseller">The reseller to insert into the airtable</param>
        /// <returns></returns>
        public Reseller Insert(Reseller reseller)
        {
            try
            {
                this.BeforeAddReseller(reseller);
                reseller = base.AddAirtableRow<Reseller>("Resellers", reseller);
                this.AfterAddReseller(reseller);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }

            return reseller;
        }
        /// <summary>
        /// Returns a list of CarModels
        /// </summary>
        /// <param name="view">the specific view to pull CarModels from</param>
        /// <returns>The list of CarModels from the given view</returns>
        public IEnumerable<CarModel> GetCarModels(String view)
        {
            var rows = this.GetTableAsAirtableRows("CarModels", view);
            return rows.ConvertTo<CarModel>();
        }

        /// <summary>
        /// Update the given carmodel
        /// </summary>
        /// <param name="carmodel">The carmodel to update</param>
        public void Update(CarModel carmodel)
        {
            try
            {
                this.BeforeUpdateCarModel(carmodel);
                this.UpdateAirtableRow<CarModel>("CarModels", carmodel);
                this.AfterUpdateCarModel(carmodel);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Delete the given carmodel
        /// </summary>
        /// <param name="carmodel">The carmodel to delete</param>
        public void Delete(CarModel carmodel)
        {
            try
            {
                this.BeforeDeleteCarModel(carmodel);
                this.DeleteAirtableRow<CarModel>("CarModels", carmodel);
                this.AfterDeleteCarModel(carmodel);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }
        }

        /// <summary>
        /// Insert a new carmodel into the airtable
        /// </summary>
        /// <param name="carmodel">The carmodel to insert into the airtable</param>
        /// <returns></returns>
        public CarModel Insert(CarModel carmodel)
        {
            try
            {
                this.BeforeAddCarModel(carmodel);
                carmodel = base.AddAirtableRow<CarModel>("CarModels", carmodel);
                this.AfterAddCarModel(carmodel);
            }
            catch (SkipOperationException soe)
            {
                // Ignore soe exceptions
            }

            return carmodel;
        }
    }
}
